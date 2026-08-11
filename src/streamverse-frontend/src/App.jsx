import { useState, useEffect } from 'react'
import axios from 'axios'

const API = 'https://localhost:7276/api'

function App() {
    const [movies, setMovies] = useState([])
    const [series, setSeries] = useState([])
    const [genres, setGenres] = useState([])
    const [activeTab, setActiveTab] = useState('movies')
    const [search, setSearch] = useState('')
    const [selectedGenre, setSelectedGenre] = useState(null)
    const [ratings, setRatings] = useState([])
    const [favorites, setFavorites] = useState([])
    const [activeNav, setActiveNav] = useState('home')

    useEffect(() => {
        axios.get(`${API}/movies`).then(res => setMovies(res.data.data || []))
        axios.get(`${API}/serie`).then(res => setSeries(res.data.data || []))
        axios.get(`${API}/genres`).then(res => setGenres(res.data.data || []))
        axios.get(`${API}/ratings`).then(res => setRatings(res.data.data || []))
        axios.get(`${API}/favorites`).then(res => setFavorites(res.data.data || []))
    }, [])

    const filtered = activeTab === 'movies'
        ? movies.filter(m =>
            m.title.toLowerCase().includes(search.toLowerCase()) &&
            (selectedGenre === null || m.genreName === selectedGenre)
        )
        : series.filter(s =>
            s.title.toLowerCase().includes(search.toLowerCase()) &&
            (selectedGenre === null || s.genreName === selectedGenre)
        )

    const genreNames = [...new Set([...movies.map(m => m.genreName), ...series.map(s => s.genreName)].filter(Boolean))]

    const getAvgRating = (title) => {
        const related = ratings.filter(r => r.movieTitle === title || r.serieTitle === title)
        if (!related.length) return null
        return (related.reduce((sum, r) => sum + r.score, 0) / related.length).toFixed(1)
    }

    const isFavorite = (title) => favorites.some(f => f.movieTitle === title || f.serieTitle === title)

    return (
        <div style={{ minHeight: '100vh', background: '#0f0f0f', color: '#fff', fontFamily: 'system-ui, sans-serif' }}>

            <nav style={{ background: '#111', borderBottom: '1px solid #222', padding: '14px 32px', display: 'flex', alignItems: 'center', justifyContent: 'space-between', position: 'sticky', top: 0, zIndex: 100 }}>
                <div style={{ fontSize: 20, fontWeight: 700, letterSpacing: -0.5 }}>
                    Stream<span style={{ color: '#3b82f6' }}>Verse</span>
                </div>
                <div style={{ display: 'flex', gap: 28, fontSize: 14 }}>
                    {['home', 'movies', 'series', 'favorites'].map(nav => (
                        <button key={nav} onClick={() => { setActiveNav(nav); if (nav === 'movies') setActiveTab('movies'); if (nav === 'series') setActiveTab('series') }}
                            style={{ background: 'none', border: 'none', color: activeNav === nav ? '#fff' : '#666', fontWeight: activeNav === nav ? 600 : 400, cursor: 'pointer', fontSize: 14, textTransform: 'capitalize' }}>
                            {nav === 'home' ? 'Inicio' : nav === 'movies' ? 'Peliculas' : nav === 'series' ? 'Series' : 'Favoritos'}
                        </button>
                    ))}
                </div>
                <input type="text" placeholder="Buscar titulo..." value={search}
                    onChange={e => { setSearch(e.target.value); setActiveNav('movies') }}
                    style={{ background: '#1a1a1a', border: '1px solid #333', borderRadius: 8, padding: '8px 14px', color: '#fff', fontSize: 13, width: 200, outline: 'none' }} />
            </nav>

            {activeNav === 'home' && (
                <div style={{ background: '#1a1a2e', padding: '60px 32px 40px' }}>
                    <div style={{ maxWidth: 600 }}>
                        <div style={{ background: '#1e3a5f', color: '#60a5fa', fontSize: 12, fontWeight: 600, padding: '4px 12px', borderRadius: 20, display: 'inline-block', marginBottom: 16 }}>
                            Catalogo actualizado
                        </div>
                        <h1 style={{ fontSize: 36, fontWeight: 700, marginBottom: 12, lineHeight: 1.2 }}>Descubre tu proxima pelicula o serie favorita</h1>
                        <p style={{ color: '#888', fontSize: 15, lineHeight: 1.6, marginBottom: 24 }}>
                            Explora nuestro catalogo, valora tu contenido favorito y guarda lo que quieres ver.
                        </p>
                        <div style={{ display: 'flex', gap: 12 }}>
                            <button onClick={() => { setActiveNav('movies'); setActiveTab('movies') }}
                                style={{ background: '#3b82f6', border: 'none', borderRadius: 8, padding: '10px 22px', color: '#fff', fontWeight: 600, cursor: 'pointer', fontSize: 14 }}>
                                Ver peliculas
                            </button>
                            <button onClick={() => { setActiveNav('series'); setActiveTab('series') }}
                                style={{ background: '#1a1a1a', border: '1px solid #333', borderRadius: 8, padding: '10px 22px', color: '#fff', cursor: 'pointer', fontSize: 14 }}>
                                Ver series
                            </button>
                        </div>
                    </div>
                </div>
            )}

            {activeNav === 'home' && (
                <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: 12, padding: '24px 32px', background: '#111', borderTop: '1px solid #222', borderBottom: '1px solid #222' }}>
                    {[
                        { label: 'Peliculas', val: movies.length },
                        { label: 'Series', val: series.length },
                        { label: 'Generos', val: genres.length },
                        { label: 'Valoraciones', val: ratings.length },
                    ].map(s => (
                        <div key={s.label} style={{ textAlign: 'center' }}>
                            <div style={{ fontSize: 28, fontWeight: 700, color: '#3b82f6' }}>{s.val}</div>
                            <div style={{ fontSize: 12, color: '#666', marginTop: 4 }}>{s.label}</div>
                        </div>
                    ))}
                </div>
            )}

            {activeNav !== 'favorites' ? (
                <main style={{ padding: '32px' }}>
                    {activeNav === 'home' && (
                        <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: 20 }}>
                            <h2 style={{ fontSize: 20, fontWeight: 600 }}>
                                {activeTab === 'movies' ? 'Peliculas destacadas' : 'Series destacadas'}
                            </h2>
                            <div style={{ display: 'flex', gap: 8 }}>
                                <button onClick={() => setActiveTab('movies')}
                                    style={{ background: activeTab === 'movies' ? '#3b82f6' : '#1a1a1a', border: '1px solid #333', borderRadius: 6, padding: '6px 14px', color: '#fff', cursor: 'pointer', fontSize: 13 }}>
                                    Peliculas
                                </button>
                                <button onClick={() => setActiveTab('series')}
                                    style={{ background: activeTab === 'series' ? '#3b82f6' : '#1a1a1a', border: '1px solid #333', borderRadius: 6, padding: '6px 14px', color: '#fff', cursor: 'pointer', fontSize: 13 }}>
                                    Series
                                </button>
                            </div>
                        </div>
                    )}

                    <div style={{ display: 'flex', gap: 8, flexWrap: 'wrap', marginBottom: 24 }}>
                        <button onClick={() => setSelectedGenre(null)}
                            style={{ background: selectedGenre === null ? '#3b82f6' : '#1a1a1a', border: '1px solid #333', borderRadius: 20, padding: '5px 14px', color: '#fff', cursor: 'pointer', fontSize: 12 }}>
                            Todos
                        </button>
                        {genreNames.map(g => (
                            <button key={g} onClick={() => setSelectedGenre(g === selectedGenre ? null : g)}
                                style={{ background: selectedGenre === g ? '#3b82f6' : '#1a1a1a', border: '1px solid #333', borderRadius: 20, padding: '5px 14px', color: selectedGenre === g ? '#fff' : '#aaa', cursor: 'pointer', fontSize: 12 }}>
                                {g}
                            </button>
                        ))}
                    </div>

                    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(200px, 1fr))', gap: 16 }}>
                        {filtered.map(item => {
                            const avg = getAvgRating(item.title)
                            const fav = isFavorite(item.title)
                            return (
                                <div key={item.id} style={{ background: '#111', border: '1px solid #222', borderRadius: 12, overflow: 'hidden' }}
                                    onMouseEnter={e => e.currentTarget.style.borderColor = '#3b82f6'}
                                    onMouseLeave={e => e.currentTarget.style.borderColor = '#222'}>
                                    <div style={{ height: 140, background: '#1a1a2e', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: 48, position: 'relative' }}>
                                        {activeTab === 'movies' ? '🎬' : '📺'}
                                        {fav && <span style={{ position: 'absolute', top: 8, right: 10, fontSize: 16 }}>❤️</span>}
                                    </div>
                                    <div style={{ padding: '12px 14px' }}>
                                        <div style={{ fontWeight: 600, fontSize: 14, marginBottom: 6, whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>{item.title}</div>
                                        <div style={{ color: '#666', fontSize: 12, marginBottom: 8 }}>{item.genreName} · {item.year}</div>
                                        {activeTab === 'series' && item.seasons && (
                                            <div style={{ color: '#555', fontSize: 11, marginBottom: 8 }}>{item.seasons} temp. · {item.episodes} ep.</div>
                                        )}
                                        <div>
                                            {avg ? (
                                                <span style={{ color: '#f59e0b', fontSize: 12, fontWeight: 600 }}>⭐ {avg}</span>
                                            ) : (
                                                <span style={{ color: '#444', fontSize: 11 }}>Sin valorar</span>
                                            )}
                                        </div>
                                    </div>
                                </div>
                            )
                        })}
                    </div>

                    {filtered.length === 0 && (
                        <div style={{ textAlign: 'center', color: '#444', padding: '60px 0' }}>
                            <div style={{ fontSize: 40, marginBottom: 12 }}>🔍</div>
                            <p>No se encontraron resultados</p>
                        </div>
                    )}
                </main>
            ) : (
                <main style={{ padding: '32px' }}>
                    <h2 style={{ fontSize: 20, fontWeight: 600, marginBottom: 24 }}>❤️ Mis Favoritos</h2>
                    {favorites.length === 0 ? (
                        <div style={{ textAlign: 'center', color: '#444', padding: '60px 0' }}>
                            <div style={{ fontSize: 40, marginBottom: 12 }}>💔</div>
                            <p>No tienes favoritos todavia</p>
                        </div>
                    ) : (
                        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(200px, 1fr))', gap: 16 }}>
                            {favorites.map(f => (
                                <div key={f.id} style={{ background: '#111', border: '1px solid #222', borderRadius: 12, overflow: 'hidden' }}>
                                    <div style={{ height: 140, background: '#1a1a2e', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: 48 }}>
                                        {f.movieTitle ? '🎬' : '📺'}
                                    </div>
                                    <div style={{ padding: '12px 14px' }}>
                                        <div style={{ fontWeight: 600, fontSize: 14, marginBottom: 4 }}>{f.movieTitle || f.serieTitle}</div>
                                        <div style={{ color: '#666', fontSize: 12 }}>{f.userName}</div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </main>
            )}
        </div>
    )
}

export default App  