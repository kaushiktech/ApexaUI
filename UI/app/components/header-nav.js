import Component from '@glimmer/component';
import { inject as service } from '@ember/service';
import { action } from '@ember/object';

export default class HeaderNav extends Component {
  @service router;
  @service PopUp;
  myFun() {
    this.testFun();
  }
  get activeRoute() {
    return this.router.currentRouteName == null
      ? ''
      : this.router.currentRouteName;
  }
  @action redirect(route) {
    this.router.transitionTo(route);
  }
  @action openPopup() {
    this.PopUp.updateIt(true);
  }
  @action testFun() {
    console.log('omg');
  }
}
