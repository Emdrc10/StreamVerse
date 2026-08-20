import axios from 'axios'

const API = 'https://localhost:7276/api'

// Permitir certificados autofirmados
axios.defaults.httpsAgent = { rejectUnauthorized: false }

export const moviesApi = {
  getAll: () => axios.get(`${API}/Movies`),
  create: (data) => axios.post(`${API}/Movies`, data),
  update: (id, data) => axios.put(`${API}/Movies/${id}`, data),
  delete: (id) => axios.delete(`${API}/Movies/${id}`)
}

export const seriesApi = {
  getAll: () => axios.get(`${API}/Serie`),
  create: (data) => axios.post(`${API}/Serie`, data),
  update: (id, data) => axios.put(`${API}/Serie/${id}`, data),
  delete: (id) => axios.delete(`${API}/Serie/${id}`)
}

export const genresApi = {
  getAll: () => axios.get(`${API}/Genres`),
  create: (data) => axios.post(`${API}/Genres`, data),
  update: (id, data) => axios.put(`${API}/Genres/${id}`, data),
  delete: (id) => axios.delete(`${API}/Genres/${id}`)
}

export const favoritesApi = {
  getAll: () => axios.get(`${API}/Favorites`),
  create: (userId, movieId, serieId) => axios.post(`${API}/Favorites`, { userId, movieId, serieId }),
  delete: (id) => axios.delete(`${API}/Favorites/${id}`)
}

export const ratingsApi = {
  getAll: () => axios.get(`${API}/Ratings`),
  create: (userId, movieId, serieId, score, review = '') =>
    axios.post(`${API}/Ratings`, { userId, movieId, serieId, score, review }),
  delete: (id) => axios.delete(`${API}/Ratings/${id}`)
}