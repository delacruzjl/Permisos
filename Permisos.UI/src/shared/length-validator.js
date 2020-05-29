export const valueValidator = {
  stringValidate: function(obj, minimumLength) {
    return obj && obj.trim().length > (minimumLength || 0);
  },
  intValidate: function(obj, minimumInt) {
    return obj && obj > (minimumInt || 0);
  }
};
