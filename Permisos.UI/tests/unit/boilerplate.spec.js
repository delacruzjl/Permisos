/* eslint-disable no-unused-vars */
/* eslint no-use-before-define: 0 */ // --> OFF
// @ts-ignore
import 'mocha';
import * as chai from 'chai';
import sinon from 'sinon';

import { createLocalVue, shallowMount } from '@vue/test-utils';
import VueRouter from 'vue-router';

import { dataService } from '../../src/shared';
// TODO: import COMOPONENT from 'PATH';
const mock = sinon.mock(dataService);

describe.skip('boilerplate code for testing', () => {
  let wrapper;
  let localVue, router;

  beforeEach(() => {
    localVue = createLocalVue();
    localVue.use(VueRouter);
    const routes = [{ name: 'realname', path: '/fakepath' }];
    router = new VueRouter({ routes });
  });

  beforeEach(() => {
    mock
      .expects('getTipoPermisos')
      .once()
      .resolves([1, 2]);

    mock
      .expects('addPermiso')
      .once()
      .resolves(true);

    // wrapper = shallowMount(COMPONENT, {
    //   localVue,
    //   router
    // });
  });

  it('n/a', done => {
    done();
    wrapper.should.be.an('object');
  });

  afterEach(() => {
    // Restore the default sandbox here
    mock.restore();
    wrapper = undefined;
    localVue = undefined;
    router = undefined;
  });
});
