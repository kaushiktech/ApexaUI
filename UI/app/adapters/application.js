import RESTAdapter from '@ember-data/adapter/rest';
import { computed } from '@ember/object';
import { inject as service } from '@ember/service';

export default class ApplicationAdapter extends RESTAdapter {
  @service store;
  namespace = 'api';
  host = 'https://localhost:7019';
  @service session;
  @computed(
    'session.data.authenticated.token'
  )
  
  get headers() {
    let headers = {};
    if (this.session.isAuthenticated) {
      headers['Authorization'] =
        `Bearer ${this.session.data.authenticated.token}`;
    }
    return headers;
  }
  handleResponse(status, headers, _payload, requestData) {
    if(status === 401) {
        this.session.invalidate();
    }
    return super.handleResponse(status,headers,_payload,requestData);
  }
}
