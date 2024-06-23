import psycopg2
import psycopg2.extras
from deep_translator import GoogleTranslator

def connect_to_db():
    conn = psycopg2.connect(
        dbname="reasn",
        user="dba",
        password="sql",
        host="postgres",
        port="5432"
    )
    return conn

def get_untranslated_tags_and_interests(conn):
    with conn.cursor() as cur:
        query_tags = """
        SELECT DISTINCT name AS name
        FROM events.tag
        WHERE name NOT IN (
            SELECT pl
            FROM common.translation
        );
        """
        query_interests = """
        SELECT DISTINCT name AS name
        FROM users.interest
        WHERE name NOT IN (
            SELECT pl
            FROM common.translation
        );
        """
        cur.execute(query_tags)
        tags = cur.fetchall()
        cur.execute(query_interests)
        interests = cur.fetchall()
        
        combined = set(tag[0] for tag in tags) | set(interest[0] for interest in interests)
        return list(combined)

def translate_to_english(texts):
    translator = GoogleTranslator(source='auto', target='en')
    translations = [translator.translate(text) for text in texts]
    return translations

def save_translations(conn, original_texts, translated_texts):
    with conn.cursor() as cur:
        insert_query = """
        INSERT INTO common.translation (pl, ang)
        VALUES (%s, %s)
        """
        for original, translated in zip(original_texts, translated_texts):
            cur.execute(insert_query, (original, translated))
    conn.commit()

def main():
    conn = connect_to_db()
    try:
        untranslated = get_untranslated_tags_and_interests(conn)
        
        translated = translate_to_english(untranslated)
        
        save_translations(conn, untranslated, translated)
        
    finally:
        conn.close()

if __name__ == "__main__":
    main()