<template>
  <div class="flex flex-col items-center justify-center h-screen">
    <div class="flex flex-wrap justify-center">
      <AlumnoList @edit="handleEdit" />
      <AlumnoForm :selectedAlumno="selectedAlumno" @save="handleSave" />
      </div>
    </div>
</template>

<script>
import { ref } from 'vue';
import AlumnoList from '../components/AlumnoList.vue';
import AlumnoForm from '../components/AlumnoForm.vue';

export default {
  components: { AlumnoList, AlumnoForm },
  setup() {
    const selectedAlumno = ref(null);

    const handleEdit = (alumno) => {
      selectedAlumno.value = alumno;
    };

    const handleSave = () => {
      selectedAlumno.value = null;
    };

    return { selectedAlumno, handleEdit, handleSave };
  },
};
</script>
