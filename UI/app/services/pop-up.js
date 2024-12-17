import Service from '@ember/service';
import { action } from '@ember/object';
import { tracked } from '@glimmer/tracking';

export default class PopUpService extends Service {
  @tracked isOpen = false;

  updateIt(isOpen) {
    this.isOpen = isOpen;
  }
}
