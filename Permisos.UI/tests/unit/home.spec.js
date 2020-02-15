import { createLocalVue, mount } from '@vue/test-utils';
import VueRouter from 'vue-router'
import Home from './../../src/views/Home';
import { dataService } from '../../src/shared';
import * as chai from 'chai';
import sinon from 'sinon';

const svcMock = sinon.mock(dataService);

describe('Home: ', () => {
  describe('created on success', () => {
    let wrapper;

    beforeEach(() => {
      svcMock
        .expects('getTipoPermisos')
        .once()
        .resolves([1, 2]);
      wrapper = mount(Home);
    });

    afterEach(() => {
      svcMock.restore();
      wrapper = undefined;
    });

    it('should populate tipo permisos array', done => {
      done();
      chai.expect(wrapper.vm.$data.tipoPermisos.length).to.equal(2);
    });

    it('has a created hook', () => {
      chai.assert.typeOf(Home.created, 'function');
    });

    it('sets the correct default data', () => {
      chai.assert.typeOf(Home.data, 'function');
      const defaultData = Home.data();
      chai.expect(defaultData.dateFormat).to.equal('YYYY-MM-DD');
    });

    it('correctly sets the message when created', () => {
      chai
        .expect(wrapper.vm.$data.message)
        .to.equal(
          'Estamos procesando su solicitud, espere un momento por favor...'
        );
    });

    it('should NOT set hasErrors', () => {
      chai.expect(!!wrapper.vm.$data.hasErrors).to.equal(false);
    });
  });

  describe('created on error', () => {
    const ErrorMessage = 'fake exception';

    let wrapper;
    beforeEach(() => {
      svcMock
        .expects('getTipoPermisos')
        .once()
        .rejects(ErrorMessage);
      wrapper = mount(Home);
    });

    afterEach(() => {
      // Restore the default sandbox here
      svcMock.restore();
      wrapper = undefined;
    });

    it('should NOT have populated tipo permisos array', done => {
      done();
      chai.expect(wrapper.vm.$data.tipoPermisos.length).to.equal(0);
    });

    it('should set hasErrors true', done => {
      done();
      chai.expect(wrapper.vm.$data.hasErrors).to.equal(true);
    });

    it('should populate the error message', done => {
      done();
      chai.expect(wrapper.vm.$data.message).to.equal(ErrorMessage);
    });
  });

  
  describe('ok', () => {
    let wrapper;
    let localVue, router;

    beforeEach(()=>{
      localVue = createLocalVue();
      localVue.use(VueRouter);
      const routes = [
        {name: 'ver', path: '/ver'}
      ];
      router = new VueRouter({routes});
    });
        
    beforeEach(() => {
      svcMock
        .expects('getTipoPermisos')
        .once()
        .resolves([1, 2]);

      svcMock.expects('addPermiso').once()
      .resolves(true);

      wrapper = mount(Home, {
       localVue, router
      });
    });

    afterEach(() => {
      // Restore the default sandbox here
      svcMock.restore();
      wrapper = undefined;
      localVue = undefined;
      router = undefined;
    });

    it('should clear error fields', async () => {
      const fakePermiso = {};
      await wrapper.vm.ok(fakePermiso);

      chai.expect(wrapper.vm.loading).to.equal(false);
      chai.expect(wrapper.vm.$data.hasErrors).to.not.equal(true);
    });
  });
  
});
