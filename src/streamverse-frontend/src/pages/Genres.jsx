import { useState } from 'react'
import { genresApi } from '../services/api'

function Genres({ genres, onRefresh }) {
  const [showForm, setShowForm] = useState(false)
  const [form, setForm] = useState({})
  const [editing, setEditing] = useState(null)

  const handleSave = async () => {
    try {
      if (editing) {
        await genresApi.update(editing.id, form)
      } else {
        await genresApi.create(form)
      }
      setForm({})
      setEditing(null)
      setShowForm(false)
      onRefresh()
    } catch (error) {
      alert('Error: ' + error.message)
    }
  }

  const handleDelete = async (id) => {
    if (confirm('¿Eliminar género?')) {
      try {
        await genresApi.delete(id)
        onRefresh()
      } catch (error) {
        alert('Error: ' + error.message)
      }
    }
  }

  const startEdit = (g) => {
    setForm(g)
    setEditing(g)
    setShowForm(true)
  }

  return (
    <main style={{ padding: '32px', maxWidth: '800px', margin: '0 auto' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
        <h2>📂 Géneros</h2>
        <button onClick={() => { setShowForm(!showForm); setForm({}); setEditing(null) }}
          style={{ background: '#3b82f6', color: '#fff', border: 'none', padding: '10px 20px', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
          {showForm ? 'Cancelar' : '+ Agregar'}
        </button>
      </div>

      {showForm && (
        <div style={{ background: '#111', border: '1px solid #333', padding: '20px', borderRadius: '8px', marginBottom: '24px' }}>
          <h3>{editing ? 'Editar' : 'Nuevo'} Género</h3>
          
          <input placeholder="Nombre" value={form.name || ''} onChange={e => setForm({ ...form, name: e.target.value })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <input placeholder="Descripción" value={form.description || ''} onChange={e => setForm({ ...form, description: e.target.value })}
            style={{ width: '100%', padding: '10px', marginBottom: '10px', background: '#1a1a1a', border: '1px solid #333', borderRadius: '4px', color: '#fff', boxSizing: 'border-box' }} />
          
          <button onClick={handleSave}
            style={{ width: '100%', background: '#10b981', color: '#fff', border: 'none', padding: '10px', borderRadius: '4px', cursor: 'pointer', fontWeight: 'bold' }}>
            {editing ? 'Actualizar' : 'Crear'}
          </button>
        </div>
      )}

      <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
        {genres.map(g => (
          <div key={g.id} style={{ background: '#111', border: '1px solid #333', borderRadius: '8px', padding: '16px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div>
              <div style={{ fontSize: '16px', fontWeight: 'bold' }}>{g.name}</div>
              <div style={{ fontSize: '14px', color: '#888' }}>{g.description}</div>
            </div>
            <div style={{ display: 'flex', gap: '8px' }}>
              <button onClick={() => startEdit(g)}
                style={{ background: '#3b82f6', color: '#fff', border: 'none', padding: '8px 12px', borderRadius: '4px', cursor: 'pointer', fontSize: '12px' }}>
                Editar
              </button>
              <button onClick={() => handleDelete(g.id)}
                style={{ background: '#ef4444', color: '#fff', border: 'none', padding: '8px 12px', borderRadius: '4px', cursor: 'pointer', fontSize: '12px' }}>
                Borrar
              </button>
            </div>
          </div>
        ))}
      </div>
    </main>
  )
}

export default Genres