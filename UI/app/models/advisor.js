import Model, { attr } from '@ember-data/model';
export default class AdvisorModel extends Model {
  @attr('string') fullName;
  @attr('string') sin;
  @attr('string') address;
  @attr('string') phoneNumber;
  @attr('string') displayHealthStatus;
}
