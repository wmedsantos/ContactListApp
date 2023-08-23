import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PersonAddEditComponent } from './person-add-edit/person-add-edit.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ContactListApp';

  constructor (private _dialog: MatDialog) {}

  openAddEditPersonForm(){
    this._dialog.open(PersonAddEditComponent)
  }
}
