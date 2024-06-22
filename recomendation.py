from flask import Flask, request, jsonify
import psycopg2
import psycopg2.extras

app = Flask(__name__)

def connect_to_db():
    conn = psycopg2.connect(
        dbname="reasn",
        user="dba",
        password="sql",
        host="localhost",
        port="5432"
    )
    return conn

def get_similar_tags_from_db(conn, interests):
    with conn.cursor(cursor_factory=psycopg2.extras.DictCursor) as cur:
        query = """
        SELECT DISTINCT tag_name
        FROM common.related
        WHERE interest_name = ANY(%s) AND value > 0.3
        """
        cur.execute(query, (interests,))
        results = cur.fetchall()
        
        if not results:
            return None 
        
        return [row['tag_name'] for row in results]

@app.route('/similar-tags', methods=['POST'])
def get_similar_tags():
    data = request.get_json()
    
    if isinstance(data, list):
        interests = data  # Treat data as a list of interests directly
    elif isinstance(data, dict):
        interests = data.get('interests', [])
    else:
        return jsonify({'error': 'Invalid JSON data format'}), 400

    if not interests:
        return jsonify({'error': 'No interests provided'}), 400

    conn = connect_to_db()
    try:
        similar_tags = get_similar_tags_from_db(conn, interests)
        
        if similar_tags is None:
            return jsonify([])  
        
        return jsonify(similar_tags)
    finally:
        conn.close()

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)