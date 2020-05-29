import sinon from 'sinon';

import { shallowMount } from '@vue/test-utils';

import { dataService } from '../../src/shared';
import VerPage from '../../src/views/Ver';
const mock = sinon.mock(dataService);

describe('Ver', () => {
  let wrapper;
  const fakePermiso = {
    descripcion: 'fake',
    nombreEmpleado: 'fake',
    apellidosEmpleado: 'fake',
    fechaPermiso: new Date(),
    tipoPermisoId: 1
  };

  beforeEach(() => {
    mock
      .expects('getTipoPermisos')
      .once()
      .resolves([
        { id: 1, descripcion: 'fake1' },
        { id: 2, descripcion: 'fake2' }
      ]);

    mock
      .expects('getPermisos')
      .once()
      .resolves([fakePermiso]);

    mock
      .expects('addPermiso')
      .once()
      .resolves(true);

    wrapper = shallowMount(VerPage);
  });

  it('should retrieve the list of permisos', done => {
    done();
    mock.verify();
    wrapper.vm.$data.permisos.length.should.be(1);
  });

  describe('removePermiso', () => {
    it('should return error if permisoId not found', done => {
      mock
        .expects('removePermiso')
        .once()
        .rejects('fake exception');

      wrapper.vm.remove({});
      done();

      wrapper.vm.$data.hasErrors.should.be(true);
    });

    it('should delete the permiso and update', done => {
      mock
        .expects('removePermiso')
        .once()
        .resolves(true);

      wrapper.vm.remove(fakePermiso);
      done();

      wrapper.vm.$data.permisos.length.should.be(0);
    });
  });

  afterEach(() => {
    // Restore the default sandbox here
    mock.restore();
    wrapper = undefined;
  });
});
