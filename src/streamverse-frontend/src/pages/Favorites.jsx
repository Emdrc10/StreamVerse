import { favoritesApi } from '../services/api'

function Favorites({ favorites, onRefresh }) {
  const handleDelete = async (id) => {
    if (confirm('¿Eliminar de favoritos?')) {
      try {
        await favoritesApi.delete(id)
        onRefresh()
      } catch (error) {
        alert('Error: ' + error.message)
      }
    }
  }

  return (
    <main style={{ padding: '32px', maxWidth: '1200px', margin: '0 auto' }}>
      <h2 style={{ fontSize: '28px', fontWeight: 'bold', marginBottom: '24px' }}>Mis Favoritos</h2>

      {!favorites || favorites.length === 0 ? (
        <p style={{ color: '#888' }}>No hay favoritos</p>
      ) : (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(180px, 1fr))', gap: '16px' }}>
          {favorites.map(f => (
            <div key={f.id} style={{ background: '#111', border: '1px solid #333', borderRadius: '12px', padding: '12px' }}>
              <div style={{ fontSize: '40px', textAlign: 'center', marginBottom: '12px' }}>
                {f.movieTitle ? '🎬' : '📺'}
              </div>
              <div style={{ fontSize: '14px', fontWeight: 'bold', marginBottom: '8px' }}>
                {f.movieTitle || f.serieTitle}
              </div>
              <button onClick={() => handleDelete(f.id)} style={{ width: '100%', background: '#ef4444', color: '#fff', border: 'none', padding: '8px', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
                Eliminar
              </button>
            </div>
          ))}
        </div>
      )}
    </main>
  )
}

export default Favorites