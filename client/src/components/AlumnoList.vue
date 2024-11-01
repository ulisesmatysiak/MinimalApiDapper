<template>
  <div class="container p-4">
    <h2 class="text-2xl font-bold mb-4 text-white">Lista de Alumnos</h2>
    <ul class="list-group" style="background-color:black">
      <li v-for="alumno in alumnos"
          :key="alumno.codigo"
          class="list-group-item d-flex justify-content-between align-items-center bg-dark text-white">
        <span>{{ alumno.nombre }}</span>
        <div class="d-flex gap-2">
          <button @click="onEdit(alumno)" class="btn btn-primary btn-sm">Editar</button>
          <button @click="deleteAlumno(alumno.codigo)" class="btn btn-danger btn-sm">Eliminar</button>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>

  import { api } from '../services/api';

  export default {
    props: {
      alumnos: {
        type: Array,
        required: true
      }
    },
    emits: ['edit', 'deleted'],
    setup(_, { emit }) {
      const deleteAlumno = async (codigo) => {
        await api.deleteAlumno(codigo);
        emit('deleted');
      };

      const onEdit = (alumno) => {
        emit('edit', alumno);
      };

      return { deleteAlumno, onEdit };
    }
  };
</script>

<style scoped>
  .bg-dark {
    background-color: black !important;
  }

  .text-white {
    color: white;
  }
</style>
