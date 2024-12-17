import RESTAdapter from '@ember-data/adapter/rest';
import { computed } from '@ember/object';
import { inject as service } from '@ember/service';

export default class ApplicationAdapter extends RESTAdapter {
  namespace = 'api';
  host = 'https://localhost:7019';
  @service session;
  @computed(
    'session.data.authenticated.token',
    'session.{isAuthenticated,token}',
  )
  get headers() {
    let headers = {};
    if (this.session.isAuthenticated) {
      headers['Authorization'] =
        `Bearer ${this.session.data.authenticated.token}`;
    }
    return headers;
  }
}
