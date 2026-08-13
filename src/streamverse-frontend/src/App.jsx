import { useState, useEffect } from 'react'
import NavBar from './components/NavBar'
import Home from './pages/Home'
import Movies from './pages/Movies'
import Series from './pages/Series'
import Genres from './pages/Genres'
import Favorites from './pages/Favorites'
import Reviews from './pages/Reviews'
import { moviesApi, seriesApi, genresApi, favoritesApi, ratingsApi } from './services/api'

function App() {
  const [page, setPage] = useState('home')
  const [movies, setMovies] = useState([])
  const [series, setSeries] = useState([])
  const [genres, setGenres] = useState([])
  const [favorites, setFavorites] = useState([])
  const [ratings, setRatings] = useState([])

  const loadData = () => {
    moviesApi.getAll().then(res => setMovies(res.data?.data || []))
    seriesApi.getAll().then(res => setSeries(res.data?.data || []))
    genresApi.getAll().then(res => setGenres(res.data?.data || []))
    favoritesApi.getAll().then(res => setFavorites(res.data?.data || []))
    ratingsApi.getAll().then(res => setRatings(res.data?.data || []))
  }

  useEffect(() => {
    loadData()
  }, [])

  const addFavorite = async (item, type) => {
    try {
      const isFav = favorites.some(f => 
        (type === 'movie' && f.movieTitle === item.title) || 
        (type === 'serie' && f.serieTitle === item.title)
      )
      if (isFav) {
        const fav = favorites.find(f => 
          (type === 'movie' && f.movieTitle === item.title) || 
          (type === 'serie' && f.serieTitle === item.title)
        )
        await favoritesApi.delete(fav.id)
      } else {
        await favoritesApi.create(3, type === 'movie' ? item.id : null, type === 'serie' ? item.id : null)
      }
      loadData()
    } catch (err) {
      console.error('Error favoritos:', err)
    }
  }

  const addRating = async (item, score, type, review) => {
    try {
      await ratingsApi.create(3, type === 'movie' ? item.id : null, type === 'serie' ? item.id : null, score, review)
      loadData()
    } catch (err) {
      console.error('Error rating:', err)
    }
  }

  return (
    <div style={{ minHeight: '100vh', background: '#0f0f0f', color: '#fff', fontFamily: 'Arial' }}>
      <NavBar page={page} setPage={setPage} />

      {page === 'home' && <Home movies={movies} series={series} genres={genres} ratings={ratings} favorites={favorites} onFavorite={addFavorite} onRate={addRating} />}
      {page === 'movies' && <Movies movies={movies} genres={genres} onRefresh={loadData} />}
      {page === 'series' && <Series series={series} genres={genres} onRefresh={loadData} />}
      {page === 'genres' && <Genres genres={genres} onRefresh={loadData} />}
      {page === 'reviews' && <Reviews ratings={ratings} />}
      {page === 'favorites' && <Favorites favorites={favorites} onRefresh={loadData} />}
    </div>
  )
}

export default App