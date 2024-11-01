import axios from 'axios';

const API_URL = 'https://localhost:7264/alumnos';

export const api = {
  fetchAlumnos: async () => {
    try {
      const response = await axios.get(API_URL);
      /*console.log(response.data);*/
      return response.data;
    } catch (error) {
      console.error('Error fetching alumnos:', error);
      throw new Error('Could not fetch alumnos');
    }
  },

  fetchAlumnoByCodigo: async (codigo) => {
    try {
      const response = await axios.get(`${API_URL}/${codigo}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching alumno with codigo ${codigo}:`, error);
      throw new Error(`Could not fetch alumno with codigo ${codigo}`);
    }
  },

  createAlumno: async (alumno) => {
    try {
      const response = await axios.post(API_URL, alumno);
      return response.data;
    } catch (error) {
      console.error('Error creating alumno:', error);
      throw new Error('Could not create alumno');
    }
  },

  updateAlumno: async (codigo, alumno) => {
    try {
      const response = await axios.put(`${API_URL}/${codigo}`, alumno);
      return response.data;
    } catch (error) {
      console.error(`Error updating alumno with codigo ${codigo}:`, error);
      throw new Error(`Could not update alumno with codigo ${codigo}`);
    }
  },

  deleteAlumno: async (codigo) => {
    try {
      const response = await axios.delete(`${API_URL}/${codigo}`);
      return response.data;
    } catch (error) {
      console.error(`Error deleting alumno with codigo ${codigo}:`, error);
      throw new Error(`Could not delete alumno with codigo ${codigo}`);
    }
  },
};
