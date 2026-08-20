import { useState } from 'react'
import { moviesApi } from '../services/api'

function Movies({ movies, genres, onRefresh }) {
  const [showForm, setShowForm] = useState(false)
  const [form, setForm] = useState({})
  const [editing, setEditing] = useState(null)
  const [search, setSearch] = useState('')

  const filtered = movies.filter(m => m.title.toLowerCase().includes(search.toLowerCase()))

  const handleSave = async () => {
    if (!form.title || !form.genreId) {
      alert('Título y género son obligatorios')
      return
    }
    try {
      const data = {
        title: form.title,
        year: Number(form.year) || 0,
        duration: Number(form.duration) || 0,
        synopsis: form.synopsis || '',
        poster: form.poster || '',
        genreId: Number(form.genreId)
      }

      if (editing) {
        await moviesApi.update(editing.id, data)
      } else {
        await moviesApi.create(data)
      }
      setForm({})
      setEditing(null)
      setShowForm(false)
      onRefresh()
    } catch (error) {
      alert('Error: ' + JSON.stringify(error.response?.data || error.message))
    }
  }

  const handleDelete = async (id) => {
    if (confirm('¿Eliminar película?')) {
      try {
        await moviesApi.delete(id)
        onRefresh()
      } catch (error) {
        alert('Error: ' + JSON.stringify(error.response?.data || error.message))
      }
    }
  }

  const startEdit = (movie) => {
    setForm(movie)
    setEditing(movie)
    setShowForm(true)
  }

  return (
    <main style={{ padding: '32px', maxWidth: '1400px', margin: '0 auto' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
        <h2>Películas</h2>
        <button onClick={() => { setShowForm(!showForm); setForm({}); setEditing(null) }}
          style={{ background: '#3b82f6', color: '#fff', border: 'none', padding: '10px 20px', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
          {showForm ? 'Cancelar' : '+ Agregar'}
        </button>
      </div>

      <input type="text" placeholder="Buscar película..." value={search} onChange={e => setSearch(e.target.value)}
        style={{ width: '100%', padding: '10px', marginBottom: '20px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '6px', color: '#fff' }} />

      {showForm && (
        <div style={{ background: '#111', border: '1px solid #333', padding: '20px', borderRadius: '8px', marginBottom: '24px' }}>
          <h3>{editing ? 'Editar' : 'Nueva'} Película</h3>
          
          <input placeholder="Título" value={form.title || ''} onChange={e => setForm({ ...form, title: e.target.value })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <input placeholder="Año" type="number" value={form.year || ''} onChange={e => setForm({ ...form, year: parseInt(e.target.value) })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <input placeholder="Duración (min)" type="number" value={form.duration || ''} onChange={e => setForm({ ...form, duration: parseInt(e.target.value) })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <input placeholder="Sinopsis" value={form.synopsis || ''} onChange={e => setForm({ ...form, synopsis: e.target.value })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <input placeholder="URL Poster" value={form.poster || ''} onChange={e => setForm({ ...form, poster: e.target.value })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <select value={form.genreId || ''} onChange={e => setForm({ ...form, genreId: parseInt(e.target.value) })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff' }}>
            <option value="">Selecciona género</option>
            {genres.map(g => <option key={g.id} value={g.id}>{g.name}</option>)}
          </select>
          
          <button onClick={handleSave}
            style={{ width: '100%', background: '#10b981', color: '#fff', border: 'none', padding: '10px', borderRadius: '4px', cursor: 'pointer', fontWeight: 'bold' }}>
            {editing ? 'Actualizar' : 'Crear'}
          </button>
        </div>
      )}

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(180px, 1fr))', gap: '16px' }}>
        {filtered.map(movie => (
          <div key={movie.id} style={{ background: '#111', border: '1px solid #333', borderRadius: '8px', padding: '12px', cursor: 'pointer' }}>
            <div style={{ width: '100%', height: '140px', background: '#1a1a2e', borderRadius: '4px', marginBottom: '8px', overflow: 'hidden', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
              {movie.poster ? (
                <img src={movie.poster} alt={movie.title} style={{ width: '100%', height: '100%', objectFit: 'cover' }} onError={e => e.target.style.display = 'none'} />
              ) : null}
              {!movie.poster ? (
                <div style={{ fontSize: '50px' }}></div>
              ) : null}
            </div>
            <div style={{ fontSize: '14px', fontWeight: 'bold', marginBottom: '4px' }}>{movie.title}</div>
            <div style={{ fontSize: '12px', color: '#888', marginBottom: '12px' }}>{movie.genreName} • {movie.year}</div>
            <div style={{ display: 'flex', gap: '6px' }}>
              <button onClick={() => startEdit(movie)}
                style={{ flex: 1, background: '#3b82f6', color: '#fff', border: 'none', padding: '6px', borderRadius: '4px', cursor: 'pointer', fontSize: '12px' }}>
                Editar
              </button>
              <button onClick={() => handleDelete(movie.id)}
                style={{ flex: 1, background: '#ef4444', color: '#fff', border: 'none', padding: '6px', borderRadius: '4px', cursor: 'pointer', fontSize: '12px' }}>
                Borrar
              </button>
            </div>
          </div>
        ))}
      </div>
    </main>
  )
}

export default Movies