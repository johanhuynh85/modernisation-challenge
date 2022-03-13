import Controller from '@ember/controller';
import { service } from '@ember/service';
import { action } from '@ember/object';
import { tracked } from '@glimmer/tracking';

export default class TasksController extends Controller {
  @service store;

  @tracked toggleDialogue = false;
  @tracked titleLiteral = '';
  @tracked editingTask = null;

  @action
  completeItem(item) {
    item.completed = !item.completed;
    item.save().catch((err) => {
      item.rollbackAttributes();
    });
  }

  @action
  editItem(item) {
    this.editingTask = item;
    this.toggleDialogue = true;
    this.titleLiteral = 'Edit task';

    // customize function
    window.fadeToBlack();
  }

  @action
  deleteItem(item, itemIdx) {
    let self = this;
    console.log(item);
    if (confirm('Are you sure that you want to delete this task?')) {
      item.destroyRecord();
    }
  }

  @action
  addItem() {
    this.editingTask = this.store.createRecord('task', {});
    this.toggleDialogue = true;
    this.titleLiteral = 'Create task';

    // customize function
    window.fadeToBlack();
  }

  @action
  closeDialogue() {
    this.toggleDialogue = false;
    this.titleLiteral = '';
    this.editingTask.rollbackAttributes();

    // customize function
    window.fadeToWhite();
  }

  @action
  saveItem() {
    let self = this;

    if (
      self.editingTask.details == '' ||
      self.editingTask.details == undefined
    ) {
      window.snackbar('error', "One or more required fields haven't been filled in.");
      return;
    }

    self.editingTask.save().then(() => {
      self.toggleDialogue = false;
      self.titleLiteral = '';

      // customize function
      window.fadeToWhite();
    });
  }
}
