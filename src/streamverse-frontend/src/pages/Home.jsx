import { useState } from 'react'

function Home({ movies, series, genres, ratings, favorites, onFavorite, onRate }) {
  const [selectedGenre, setSelectedGenre] = useState(null)
  const [hoveredMovie, setHoveredMovie] = useState(null)
  const [hoveredSerie, setHoveredSerie] = useState(null)
  const [ratingModal, setRatingModal] = useState(null)
  const [ratingScore, setRatingScore] = useState(0)
  const [ratingReview, setRatingReview] = useState('')

  const getAvg = (title) => {
    const related = ratings.filter(r => r.movieTitle === title || r.serieTitle === title)
    if (!related.length) return null
    return (related.reduce((sum, r) => sum + r.score, 0) / related.length).toFixed(1)
  }

  const isFav = (title) => favorites.some(f => f.movieTitle === title || f.serieTitle === title)

  const filteredMovies = selectedGenre 
    ? movies.filter(m => m.genreName === selectedGenre)
    : movies

  const filteredSeries = selectedGenre 
    ? series.filter(s => s.genreName === selectedGenre)
    : series

    const handleSaveRating = () => {
    if (ratingScore > 0) {
        onRate(ratingModal, ratingScore, ratingModal.title ? 'movie' : 'serie', ratingReview)
        setRatingModal(null)
        setRatingScore(0)
        setRatingReview('')
    }
}

  return (
    <main style={{ background: '#0f0f0f', color: '#fff', minHeight: '100vh' }}>
      {/* Hero Interactivo */}
      <div style={{ background: 'linear-gradient(135deg, #1a1a2e 0%, #16213e 100%)', padding: '80px 32px', textAlign: 'center', borderBottom: '2px solid #3b82f6', position: 'relative', overflow: 'hidden' }}>
        <div style={{ position: 'absolute', top: '-50px', right: '-50px', width: '300px', height: '300px', background: 'rgba(59, 130, 246, 0.1)', borderRadius: '50%' }}></div>
        <h1 style={{ fontSize: '56px', fontWeight: 'bold', marginBottom: '12px', zIndex: 1, position: 'relative' }}>
          Stream<span style={{ color: '#3b82f6', textShadow: '0 0 20px rgba(59, 130, 246, 0.5)' }}>Verse</span>
        </h1>
        <p style={{ fontSize: '20px', color: '#aaa', marginBottom: '40px', zIndex: 1, position: 'relative' }}>
          ¡Descubre, califica y guarda tus favoritos!
        </p>
        
        <div style={{ display: 'flex', gap: '30px', justifyContent: 'center', flexWrap: 'wrap', zIndex: 1, position: 'relative' }}>
          <div style={{ background: 'rgba(255,255,255,0.05)', border: '2px solid #3b82f6', padding: '25px 40px', borderRadius: '12px', cursor: 'pointer', transition: 'all 0.3s' }}
            onMouseEnter={e => e.currentTarget.style.transform = 'scale(1.05)'}
            onMouseLeave={e => e.currentTarget.style.transform = 'scale(1)'}>
            <div style={{ fontSize: '36px', fontWeight: 'bold', color: '#3b82f6' }}>{movies.length}</div>
            <div style={{ fontSize: '14px', color: '#888', marginTop: '8px' }}>Películas</div>
          </div>
          <div style={{ background: 'rgba(255,255,255,0.05)', border: '2px solid #10b981', padding: '25px 40px', borderRadius: '12px', cursor: 'pointer', transition: 'all 0.3s' }}
            onMouseEnter={e => e.currentTarget.style.transform = 'scale(1.05)'}
            onMouseLeave={e => e.currentTarget.style.transform = 'scale(1)'}>
            <div style={{ fontSize: '36px', fontWeight: 'bold', color: '#10b981' }}>{series.length}</div>
            <div style={{ fontSize: '14px', color: '#888', marginTop: '8px' }}>Series</div>
          </div>
          <div style={{ background: 'rgba(255,255,255,0.05)', border: '2px solid #f59e0b', padding: '25px 40px', borderRadius: '12px', cursor: 'pointer', transition: 'all 0.3s' }}
            onMouseEnter={e => e.currentTarget.style.transform = 'scale(1.05)'}
            onMouseLeave={e => e.currentTarget.style.transform = 'scale(1)'}>
            <div style={{ fontSize: '36px', fontWeight: 'bold', color: '#f59e0b' }}>{favorites.length}</div>
            <div style={{ fontSize: '14px', color: '#888', marginTop: '8px' }}>Favoritos</div>
          </div>
        </div>
      </div>

      {/* Filtro de Géneros */}
      <div style={{ padding: '32px', maxWidth: '1400px', margin: '0 auto' }}>
        <h3 style={{ marginBottom: '16px', fontSize: '14px', color: '#888', textTransform: 'uppercase', letterSpacing: '2px' }}>Filtrar por género</h3>
        <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap', marginBottom: '40px' }}>
          <button onClick={() => setSelectedGenre(null)}
            style={{ background: !selectedGenre ? '#3b82f6' : '#1a1a1a', color: '#fff', border: '1px solid #333', padding: '10px 20px', borderRadius: '20px', cursor: 'pointer', transition: 'all 0.3s', fontWeight: 'bold' }}>
            Todos
          </button>
          {genres.map(g => (
            <button key={g.id} onClick={() => setSelectedGenre(g.name)}
              style={{ background: selectedGenre === g.name ? '#3b82f6' : '#1a1a1a', color: selectedGenre === g.name ? '#fff' : '#aaa', border: '1px solid #333', padding: '10px 20px', borderRadius: '20px', cursor: 'pointer', transition: 'all 0.3s' }}>
              {g.name}
            </button>
          ))}
        </div>
      </div>

      {/* Películas */}
      <div style={{ padding: '0 32px 48px 32px', maxWidth: '1400px', margin: '0 auto' }}>
        <h2 style={{ fontSize: '28px', fontWeight: 'bold', marginBottom: '24px' }}>Películas {selectedGenre && `- ${selectedGenre}`}</h2>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(180px, 1fr))', gap: '16px' }}>
          {filteredMovies.map(m => (
            <div key={m.id} 
              style={{ background: '#111', border: '1px solid #333', borderRadius: '12px', overflow: 'hidden', cursor: 'pointer', transition: 'all 0.3s', transform: hoveredMovie === m.id ? 'translateY(-8px)' : 'translateY(0)', boxShadow: hoveredMovie === m.id ? '0 20px 40px rgba(59, 130, 246, 0.2)' : 'none' }}
              onMouseEnter={() => setHoveredMovie(m.id)}
              onMouseLeave={() => setHoveredMovie(null)}>
              
              <div style={{ height: '140px', background: '#1a1a2e', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: '50px', position: 'relative', borderBottom: hoveredMovie === m.id ? '3px solid #3b82f6' : 'none' }}>
                {isFav(m.title) && <span style={{ position: 'absolute', top: 8, right: 8, fontSize: '11px', fontWeight: 'bold', color: '#fff', background: '#ef4444', borderRadius: '4px', padding: '3px 8px' }}>FAVORITO</span>}
              </div>
              
              <div style={{ padding: '14px' }}>
                <div style={{ fontSize: '14px', fontWeight: 'bold', marginBottom: '6px', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>{m.title}</div>
                <div style={{ fontSize: '12px', color: '#666', marginBottom: '8px' }}>{m.genreName} • {m.year}</div>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '12px' }}>
                  {getAvg(m.title) ? (
                    <span style={{ fontSize: '13px', color: '#f59e0b', fontWeight: 'bold' }}> {getAvg(m.title)}</span>
                  ) : (
                    <span style={{ fontSize: '11px', color: '#555' }}>Sin votos</span>
                  )}
                </div>
                
                {hoveredMovie === m.id && (
                  <div style={{ display: 'flex', gap: '8px' }}>
                    <button onClick={() => onFavorite(m, 'movie')}
                      style={{ flex: 1, background: isFav(m.title) ? '#ef4444' : '#1a1a1a', color: '#fff', border: '1px solid #333', padding: '8px', borderRadius: '6px', cursor: 'pointer', fontSize: '12px', fontWeight: 'bold' }}>
                      {isFav(m.title) ? ' Guardar' : ' Guardar'}
                    </button>
                    <button onClick={() => setRatingModal(m)}
                      style={{ flex: 1, background: '#f59e0b', color: '#000', border: 'none', padding: '8px', borderRadius: '6px', cursor: 'pointer', fontSize: '12px', fontWeight: 'bold' }}>
                       Calificar
                    </button>
                  </div>
                )}
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Series */}
      <div style={{ padding: '0 32px 48px 32px', maxWidth: '1400px', margin: '0 auto' }}>
        <h2 style={{ fontSize: '28px', fontWeight: 'bold', marginBottom: '24px' }}> Series {selectedGenre && `- ${selectedGenre}`}</h2>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(180px, 1fr))', gap: '16px' }}>
          {filteredSeries.map(s => (
            <div key={s.id} 
              style={{ background: '#111', border: '1px solid #333', borderRadius: '12px', overflow: 'hidden', cursor: 'pointer', transition: 'all 0.3s', transform: hoveredSerie === s.id ? 'translateY(-8px)' : 'translateY(0)', boxShadow: hoveredSerie === s.id ? '0 20px 40px rgba(59, 130, 246, 0.2)' : 'none' }}
              onMouseEnter={() => setHoveredSerie(s.id)}
              onMouseLeave={() => setHoveredSerie(null)}>
              
              <div style={{ height: '140px', background: '#1a1a2e', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: '50px', position: 'relative', borderBottom: hoveredSerie === s.id ? '3px solid #10b981' : 'none' }}>
                
                {isFav(s.title) && <span style={{ position: 'absolute', top: 8, right: 8, fontSize: '11px', fontWeight: 'bold', color: '#fff', background: '#ef4444', borderRadius: '4px', padding: '3px 8px' }}>FAVORITO</span>}
              </div>
              
              <div style={{ padding: '14px' }}>
                <div style={{ fontSize: '14px', fontWeight: 'bold', marginBottom: '6px', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>{s.title}</div>
                <div style={{ fontSize: '12px', color: '#666', marginBottom: '4px' }}>{s.genreName} • {s.year}</div>
                <div style={{ fontSize: '11px', color: '#555', marginBottom: '8px' }}>{s.seasons} temp • {s.episodes} ep</div>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '12px' }}>
                  {getAvg(s.title) ? (
                    <span style={{ fontSize: '13px', color: '#f59e0b', fontWeight: 'bold' }}> {getAvg(s.title)}</span>
                  ) : (
                    <span style={{ fontSize: '11px', color: '#555' }}>Sin votos</span>
                  )}
                </div>
                
                {hoveredSerie === s.id && (
                  <div style={{ display: 'flex', gap: '8px' }}>
                    <button onClick={() => onFavorite(s, 'serie')}
                      style={{ flex: 1, background: isFav(s.title) ? '#ef4444' : '#1a1a1a', color: '#fff', border: '1px solid #333', padding: '8px', borderRadius: '6px', cursor: 'pointer', fontSize: '12px', fontWeight: 'bold' }}>
                      {isFav(s.title) ? 'Guardar' : ' Guardar'}
                    </button>
                    <button onClick={() => setRatingModal(s)}
                      style={{ flex: 1, background: '#f59e0b', color: '#000', border: 'none', padding: '8px', borderRadius: '6px', cursor: 'pointer', fontSize: '12px', fontWeight: 'bold' }}>
                       Calificar
                    </button>
                  </div>
                )}
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Modal de Calificación */}
      {ratingModal && (
        <div style={{ position: 'fixed', top: 0, left: 0, right: 0, bottom: 0, background: 'rgba(0,0,0,0.7)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000 }}>
          <div style={{ background: '#111', border: '1px solid #333', borderRadius: '12px', padding: '32px', maxWidth: '400px', width: '90%' }}>
            <h2 style={{ fontSize: '20px', fontWeight: 'bold', marginBottom: '12px' }}>Calificar: {ratingModal.title}</h2>
            
            <div style={{ marginBottom: '20px' }}>
              <p style={{ fontSize: '14px', color: '#888', marginBottom: '12px' }}>¿Qué te parece?</p>
              <div style={{ display: 'flex', gap: '8px', justifyContent: 'center' }}>
                {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(n => (
                  <button key={n} onClick={() => setRatingScore(n)}
                    style={{ width: '32px', height: '32px', background: ratingScore === n ? '#f59e0b' : '#1a1a1a', border: ratingScore === n ? '2px solid #f59e0b' : '1px solid #333', borderRadius: '6px', color: '#fff', cursor: 'pointer', fontWeight: 'bold', fontSize: '12px' }}>
                    {n}
                  </button>
                ))}
              </div>
            </div>

            <div style={{ marginBottom: '20px' }}>
              <label style={{ fontSize: '14px', color: '#888', display: 'block', marginBottom: '8px' }}>Agregar una reseña (opcional)</label>
              <textarea placeholder="¿Qué te pareció?" value={ratingReview} onChange={e => setRatingReview(e.target.value)}
                style={{ width: '100%', background: '#1a1a1a', border: '1px solid #333', borderRadius: '6px', color: '#fff', padding: '10px', minHeight: '80px', boxSizing: 'border-box', fontFamily: 'Arial', outline: 'none', resize: 'none' }} />
            </div>

            <div style={{ display: 'flex', gap: '8px' }}>
              <button onClick={handleSaveRating}
                style={{ flex: 1, background: '#10b981', color: '#fff', border: 'none', padding: '12px', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
                Guardar Calificación
              </button>
              <button onClick={() => setRatingModal(null)}
                style={{ flex: 1, background: '#1a1a1a', color: '#fff', border: '1px solid #333', padding: '12px', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
                Cancelar
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Footer */}
      <div style={{ background: '#111', borderTop: '1px solid #333', padding: '40px 32px', marginTop: '60px', textAlign: 'center', color: '#888' }}>
        <h3 style={{ fontSize: '18px', fontWeight: 'bold', color: '#fff', marginBottom: '12px' }}>StreamVerse</h3>
        <p>Tu plataforma de películas y series interactiva</p>
        <p style={{ fontSize: '12px', marginTop: '12px', color: '#555' }}>Explora, califica, guarda y comparte</p>
      </div>
    </main>
  )
}

export default Home
