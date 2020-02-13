// @ts-nocheck
import * as axios from 'axios';
import moment from 'moment';

const inputDateFormat = 'YYYY-MM-DD';
const API = process.env.VUE_APP_API;

const getTipoPermisos = async function() {
  try {
    const response = await axios.get(`${API}/tipopermisos`);
    let data = parseList(response);
    return data;
  } catch (error) {
    window.console.error(error);
    return [];
  }
};

const getPermisos = async function() {
  try {
    const response = await axios.get(`${API}/permisos`);
    let data = parseList(response);
      data = data.map(p => {
          p.fechaPermiso = moment(p.fechaPermiso, inputDateFormat).toISOString();
      return p;
    });
    return data;
  } catch (error) {
    window.console.error(error);
    return [];
  }
};

const addPermiso = async function(permiso) {
  try {
    const response = await axios.post(`${API}/permisos`, permiso);
    if (response.status !== 201) {
      throw Error(response.message);
    }

    let data = response.data;
    return data;
  } catch (error) {
      throw error;
  }
};

const removePermiso = async function(permisoId) {
  try {
    const response = await axios.delete(`${API}/permisos/${permisoId}`);
    if (response.status !== 200) {
      throw Error(response.message);
    }
    return true;
  } catch (error) {
    window.console.error(error);
    return false;
  }
};

const parseList = response => {
  if (response.status !== 200) throw Error(response.message);
  if (!response.data) return [];
  let list = response.data;
  if (typeof list !== 'object') {
    list = [];
  }
  return list;
};

export const dataService = {
  getTipoPermisos,
  getPermisos,
  addPermiso,
  removePermiso
};
