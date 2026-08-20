import { ratingsApi } from '../services/api'

function Reviews({ ratings, onRefresh }) {
  const handleDelete = async (id) => {
    if (confirm('¿Eliminar reseña?')) {
      try {
        await ratingsApi.delete(id)
        onRefresh()
      } catch (error) {
        alert('Error: ' + JSON.stringify(error.response?.data || error.message))
      }
    }
  }

  return (
    <main style={{ padding: '32px', maxWidth: '1000px', margin: '0 auto' }}>
      <h2 style={{ fontSize: '28px', fontWeight: 'bold', marginBottom: '24px' }}>Todas las Reseñas</h2>

      {!ratings || ratings.length === 0 ? (
        <div style={{ textAlign: 'center', color: '#888', padding: '60px 0' }}>
          <p>No hay reseñas aún</p>
        </div>
      ) : (
        <>
          <p style={{ color: '#888', marginBottom: '24px' }}>Total: {ratings.length} reseñas</p>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
            {ratings.map(r => (
              <div key={r.id} style={{ background: '#111', border: '1px solid #333', borderRadius: '12px', padding: '20px' }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'start', marginBottom: '12px' }}>
                  <div>
                    <div style={{ fontSize: '16px', fontWeight: 'bold', marginBottom: '4px' }}>
                      {r.movieTitle || r.serieTitle || 'Sin título'}
                    </div>
                    <div style={{ fontSize: '12px', color: '#888' }}>
                      Por: {r.userName || 'Anónimo'}
                    </div>
                  </div>
                  <div style={{ fontSize: '28px', color: '#f59e0b', fontWeight: 'bold' }}>
                    {r.score}
                  </div>
                </div>

                {r.review && (
                  <div style={{ background: '#1a1a1a', border: '1px solid #222', borderRadius: '8px', padding: '12px', marginBottom: '12px', color: '#ccc', fontSize: '14px', lineHeight: '1.6' }}>
                    "{r.review}"
                  </div>
                )}

                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <div style={{ fontSize: '12px', color: '#666' }}>
                    {r.movieTitle ? 'Película' : 'Serie'}
                  </div>
                  <button onClick={() => handleDelete(r.id)}
                    style={{ background: '#ef4444', color: '#fff', border: 'none', padding: '8px 14px', borderRadius: '6px', cursor: 'pointer', fontSize: '12px', fontWeight: 'bold' }}>
                    Borrar
                  </button>
                </div>
              </div>
            ))}
          </div>
        </>
      )}
    </main>
  )
}

export default Reviews
