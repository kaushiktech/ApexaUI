import Component from '@glimmer/component';
import { inject as service } from '@ember/service';
import { action } from '@ember/object';
export default class Popup extends Component {
  @service PopUp;
  @service router;
  @action update() {
    this.PopUp.updateIt(!this.PopUp.isOpen);
  }
  @action login(){
    this.router.transitionTo('index');
  }
}
