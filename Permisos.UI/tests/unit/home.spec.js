/* eslint-disable no-console */
import { expect } from 'chai';
import { mount } from '@vue/test-utils';
// @ts-ignore
import Home from '../../src/views/Home.vue';
import sinon from 'sinon';
import { dataService } from '../../src/shared';

describe('Home.vue', () => {
  var sandbox = sinon.createSandbox();
  let wrapper;

  beforeEach(() => {
    sandbox.spy(dataService, 'addPermiso');
  });

  beforeEach(() => {
    wrapper = mount(Home, {
      propsData: {
        dataService: dataService
      }
    });
  });

  afterEach(() => {
    wrapper.destroy();
    sandbox.restore();
  });

  it('verifies a submit button exists', () => {
    // arrange
    const $button = wrapper.find('button[type="submit"]');

    // act

    // assert
    expect(true).to.equal($button.is('button'));
  });

  it('submit click, ok method should call the service', async () => {
    console.log(wrapper.find('button[type="submit"]').att);
  });
});
