<template>
  <div class="flex flex-col items-center justify-center h-screen">
    <div class="flex flex-wrap justify-center">
      <AlumnoList :alumnos="alumnos" @edit="handleEdit" @deleted="fetchAlumnos" />
      <AlumnoForm :selectedAlumno="selectedAlumno" @save="handleSave" />
    </div>
  </div>
</template>

<script>
  import { ref, onMounted } from 'vue';
  import AlumnoList from '../components/AlumnoList.vue';
  import AlumnoForm from '../components/AlumnoForm.vue';
  import { api } from '../services/api';

  export default {
    components: { AlumnoList, AlumnoForm },
    setup() {
      const selectedAlumno = ref(null);
      const alumnos = ref([]);

      const fetchAlumnos = async () => {
        const response = await api.fetchAlumnos();
        alumnos.value = response;
      };

      const handleEdit = (alumno) => {
        selectedAlumno.value = alumno;
      };

      const handleSave = async () => {
        await fetchAlumnos();
        selectedAlumno.value = null;
      };

      onMounted(fetchAlumnos);

      return { selectedAlumno, handleEdit, handleSave, fetchAlumnos, alumnos };
    },
  };
</script>
