import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { PersonService } from '../services/person.service';
import { DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'app-person-add-edit',
  templateUrl: './person-add-edit.component.html',
  styleUrls: ['./person-add-edit.component.scss']
})
export class PersonAddEditComponent {
  personForm: FormGroup;

  constructor(
      private fb: FormBuilder,
      private _personService: PersonService,
      private _dialog: DialogRef<PersonAddEditComponent>)
    {

    this.personForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      sex: '',
      age: ['', Validators.min(0)],
      contacts: this.fb.array([]),
    });
  }

  // ngOnInit(): void {
  //   this.personForm = this.fb.group({
  //     // ... other form controls ...
  //     contacts: this.fb.array([])
  //   });
 //}

  getControls() {
    return (this.personForm.get('contacts') as FormArray).controls;
  }
  get contactFormArray(): FormArray {
    return this.personForm.get('contacts') as FormArray;
  }
  addBlankContact(): void {
    const newContact = this.fb.group({
      type: [''],
      contactValue: ['']
    });
    this.contactFormArray.push(newContact);
  }

  removeContact(index: number): void {
    this.contactFormArray.removeAt(index);
  }

  onFormSubmit(){
    //if(this.personForm.valid){
      console.log(this.personForm.value);
      this._personService.addPerson(this.personForm.value).subscribe(
        {
          next: (val: any) => {
            alert('Sucses');
            this._dialog.close();
          },
          error: (err: any) => {
            console.log(err)
          }
        });

   // }
  }
}
