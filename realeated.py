import psycopg2
from sentence_transformers import SentenceTransformer
from sklearn.metrics.pairwise import cosine_similarity

def connect_to_db():
    conn = psycopg2.connect(
        dbname="reasn",
        user="dba",
        password="sql",
        host="localhost",
        port="5432"
    )
    return conn

def get_translated_tags_and_interests(conn):
    with conn.cursor() as cur:
        query_translated_tags = """
        SELECT t.name, tr.name_ang
        FROM events.tag t
        JOIN common.translated tr ON t.name = tr.name_pl
        """
        query_translated_interests = """
        SELECT i.name, tr.name_ang
        FROM users.interest i
        JOIN common.translated tr ON i.name = tr.name_pl
        """
        cur.execute(query_translated_tags)
        tags = cur.fetchall()
        
        cur.execute(query_translated_interests)
        interests = cur.fetchall()
    
    return tags, interests

def get_missing_tags_and_interests(conn):
    with conn.cursor() as cur:
        query_missing_tags = """
        SELECT name
        FROM events.tag
        WHERE name NOT IN (
            SELECT tag_name
            FROM common.related
        );
        """
        query_missing_interests = """
        SELECT name
        FROM users.interest
        WHERE name NOT IN (
            SELECT interest_name
            FROM common.related
        );
        """
        cur.execute(query_missing_tags)
        missing_tags = [row[0] for row in cur.fetchall()]
        
        cur.execute(query_missing_interests)
        missing_interests = [row[0] for row in cur.fetchall()]
    
    return missing_tags, missing_interests

def calculate_semantic_similarity_sbert(interests, tags):
    model = SentenceTransformer('all-MiniLM-L6-v2')

    interest_embeddings = model.encode(interests)
    tag_embeddings = model.encode(tags)

    similarities = cosine_similarity(interest_embeddings, tag_embeddings)

    return similarities

def save_similarities_to_db(conn, tags, interests, similarities):
    with conn.cursor() as cur:
        insert_query = """
        INSERT INTO common.related (tag_name, interest_name, value)
        VALUES (%s, %s, %s)
        ON CONFLICT (tag_name, interest_name) DO NOTHING
        """
        for i, tag in enumerate(tags):
            for j, interest in enumerate(interests):
                value = float(similarities[j][i])
                cur.execute(insert_query, (tag, interest, value))
    conn.commit()

def main():
    conn = connect_to_db()
    try:
        # Pobierz przetłumaczone tagi i zainteresowania
        translated_tags, translated_interests = get_translated_tags_and_interests(conn)
        
        # Rozdziel wyniki na oryginalne i przetłumaczone nazwy
        tags, tags_ang = zip(*translated_tags) if translated_tags else ([], [])
        interests, interests_ang = zip(*translated_interests) if translated_interests else ([], [])
        
        # Pobierz brakujące tagi i zainteresowania
        missing_tags, missing_interests = get_missing_tags_and_interests(conn)
        
        print("Missing tags:")
        for tag in missing_tags:
            print(tag)
        
        print("\nMissing interests:")
        for interest in missing_interests:
            print(interest)

        if interests_ang and missing_tags:
            similarities_missing_tags = calculate_semantic_similarity_sbert(interests_ang, missing_tags)
            save_similarities_to_db(conn, missing_tags, interests, similarities_missing_tags)
        if missing_interests and tags_ang:
            similarities_missing_interests = calculate_semantic_similarity_sbert(missing_interests, tags_ang)
            save_similarities_to_db(conn, tags, missing_interests, similarities_missing_interests)
        
        print("\nSimilarities saved successfully.")
    finally:
        conn.close()

if __name__ == "__main__":
    main()
