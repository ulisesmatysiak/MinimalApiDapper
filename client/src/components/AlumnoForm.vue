<template>
  <form @submit.prevent="handleSubmit" class="p-4">
    <h2 class="text-2xl font-bold mb-4 text-white">{{ selectedAlumno ? 'Editar Alumno' : 'Crear Alumno' }}</h2>
    <div class="input-group mb-3">
      <input v-model="nombre" type="text" placeholder="Nombre" class="form-control" />
      <button type="submit" class="btn btn-primary">{{ selectedAlumno ? 'Actualizar' : 'Crear' }}</button>
    </div>
  </form>
</template>

<script>
  import { ref, watch } from 'vue';
  import { api } from '../services/api';

  export default {
    props: {
      selectedAlumno: {
        type: Object,
        default: null,
      },
    },
    emits: ['save'],
    setup(props, { emit }) {
      const nombre = ref('');

      watch(() => props.selectedAlumno, (newAlumno) => {
        nombre.value = newAlumno ? newAlumno.nombre : '';
      }, { immediate: true });

      const handleSubmit = async () => {
        if (props.selectedAlumno) {
          await api.updateAlumno(props.selectedAlumno.codigo, { nombre: nombre.value });
        } else {
          await api.createAlumno({ nombre: nombre.value });
        }
        emit('save');
      };

      return { nombre, handleSubmit };
    },
  };
</script>
<style scoped>
  .input-group {
    display: flex;
    align-items: center;
  }

  .form-control {
    flex: 1;
    margin-right: 0.5rem; 
  }
</style>
