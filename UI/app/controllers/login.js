import Controller from '@ember/controller';
import { inject as service } from '@ember/service';
import { tracked } from '@glimmer/tracking';
import { action } from '@ember/object';

export default class LoginController extends Controller {
  @service session;
  @service PopUp;
  @service router;
  @tracked error;
  @tracked username;
  @tracked password;

  @action
  update(attr, event) {
    this[attr] = event.target.value;
  }
  @action
  async login(event) {
    console.log('loginOmg');
    event.preventDefault();
    try {
      await this.session.authenticate(
        'authenticator:jwt',
        this.username,
        this.password,
      );
    } catch (error) {
      console.log(error);
      this.error = error;
    }
  }
}
