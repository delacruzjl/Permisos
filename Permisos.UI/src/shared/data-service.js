// @ts-nocheck
import * as axios from 'axios';
import moment from 'moment';

const inputDateFormat = 'YYYY-MM-DD';
const API = process.env.VUE_APP_API;

const getTipoPermisos = async function() {
  const response = await axios.get(`${API}/tipopermisos`);
  let data = parseList(response);
  return data;
};

const getPermisos = async function() {
  const response = await axios.get(`${API}/permisos`);
  let data = parseList(response);
  data = data.map(p => {
    p.fechaPermiso = moment(p.fechaPermiso, inputDateFormat).toISOString();
    return p;
  });

  return data;
};

const addPermiso = async function(permiso) {
  const response = await axios.post(`${API}/permisos`, permiso);
  if (response.status !== 201) {
    throw new Error(response.message);
  }

  let data = response.data;
  return data;
};

const removePermiso = async function(permisoId) {
  const response = await axios.delete(`${API}/permisos/${permisoId}`);
  if (response.status !== 200) {
    throw new Error(response.message);
  }
  return true;
};

const parseList = response => {
  if (response.status !== 200) throw new Error(response.message);
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
