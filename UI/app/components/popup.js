import Component from '@glimmer/component';
import { inject as service } from '@ember/service';
import { tracked } from '@glimmer/tracking';
import { action } from '@ember/object';
export default class Popup extends Component {
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
    event.preventDefault();
    try {
      await this.session.authenticate(
        'authenticator:jwt',
        this.username,
        this.password,
      );
      this.toggleLoginPopup();
      this.router.transitionTo('index');
    } catch (error) {
      console.log('Error' + error);
      this.error = error;
    }
  }

  @action toggleLoginPopup() {
    this.PopUp.openPopup(!this.PopUp.isOpen);
  }
}
