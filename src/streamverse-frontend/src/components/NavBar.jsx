function NavBar({ page, setPage }) {
const navs = ['home', 'movies', 'series', 'genres', 'reviews', 'favorites']
  
  const getLabel = (nav) => {
    if (nav === 'home') return 'Inicio'
    if (nav === 'movies') return 'Películas'
    if (nav === 'series') return 'Series'
    if (nav === 'genres') return 'Géneros'
    if (nav === 'favorites') return 'Favoritos'
    if (nav === 'reviews') return 'Reseñas'
  }

  return (
    <nav style={{ background: '#111', padding: '16px', borderBottom: '1px solid #333', display: 'flex', gap: '20px' }}>
      <div style={{ fontSize: 18, fontWeight: 'bold', color: '#3b82f6' }}>StreamVerse</div>
      
      <div style={{ display: 'flex', gap: '20px', flex: 1 }}>
        {navs.map(nav => (
          <button key={nav} onClick={() => setPage(nav)}
            style={{ background: 'none', border: 'none', color: page === nav ? '#fff' : '#888', cursor: 'pointer', fontSize: 14, fontWeight: page === nav ? 'bold' : 'normal' }}>
            {getLabel(nav)}
          </button>
        ))}
      </div>
    </nav>
  )
}

export default NavBar