import Model, { attr } from '@ember-data/model';

export default class TaskModel extends Model {
  @attr('boolean') completed;
  @attr('string') details;
}
