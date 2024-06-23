import psycopg2
from sentence_transformers import SentenceTransformer
from sklearn.metrics.pairwise import cosine_similarity

def connect_to_db():
    conn = psycopg2.connect(
        dbname="reasn",
        user="dba",
        password="sql",
        host="postgres",
        port="5432"
    )
    return conn

def get_translated_tags_and_interests(conn):
    with conn.cursor() as cur:
        query_translated_tags = """
        SELECT t.name, tr.ang
        FROM events.tag t
        JOIN common.translation tr ON t.name = tr.pl
        """
        query_translated_interests = """
        SELECT i.name, tr.ang
        FROM users.interest i
        JOIN common.translation tr ON i.name = tr.pl
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
            SELECT tag
            FROM common.tag_interest_simularity
        );
        """
        query_missing_interests = """
        SELECT name
        FROM users.interest
        WHERE name NOT IN (
            SELECT interest
            FROM common.tag_interest_simularity
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
        INSERT INTO common.tag_interest_simularity (tag, interest, simularity)
        VALUES (%s, %s, %s)
        ON CONFLICT (tag, interest) DO NOTHING
        """
        for i, tag in enumerate(tags):
            for j, interest in enumerate(interests):
                value = float(similarities[j][i])
                cur.execute(insert_query, (tag, interest, value))
    conn.commit()

def main():
    conn = connect_to_db()
    try:
        translated_tags, translated_interests = get_translated_tags_and_interests(conn)

        tags, tags_ang = zip(*translated_tags) if translated_tags else ([], [])
        interests, interests_ang = zip(*translated_interests) if translated_interests else ([], [])
        
        missing_tags, missing_interests = get_missing_tags_and_interests(conn)
        

        if interests_ang and missing_tags:
            similarities_missing_tags = calculate_semantic_similarity_sbert(interests_ang, missing_tags)
            save_similarities_to_db(conn, missing_tags, interests, similarities_missing_tags)
        if missing_interests and tags_ang:
            similarities_missing_interests = calculate_semantic_similarity_sbert(missing_interests, tags_ang)
            save_similarities_to_db(conn, tags, missing_interests, similarities_missing_interests)
        
    finally:
        conn.close()

if __name__ == "__main__":
    main()
