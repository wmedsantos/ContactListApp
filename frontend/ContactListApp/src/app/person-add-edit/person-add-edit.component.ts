import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  FormArray } from '@angular/forms';
import { PersonService } from '../services/person.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CoreService } from '../core/core.service';

@Component({
  selector: 'app-person-add-edit',
  templateUrl: './person-add-edit.component.html',
  styleUrls: ['./person-add-edit.component.scss']
})
export class PersonAddEditComponent implements OnInit{
  personForm: FormGroup;

  constructor(
      private fb: FormBuilder,
      private _personService: PersonService,
      private _dialog: MatDialogRef<PersonAddEditComponent>,
      @Inject(MAT_DIALOG_DATA) public data: any,
      private _coreService: CoreService
      )
    {

    this.personForm = this.fb.group({
      id: null,
      name: '',//['', Validators.required],
      sex: '',
      age: '',//['', Validators.min(0)],
      contacts: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    this.personForm.patchValue (this.data);
  }
  onFormSubmit(){

    if(this.personForm.valid){
      if (this.data){
        this._personService.editPerson(this.personForm.value).subscribe(
          {
            next: (val: any) => {
              this._coreService.openSnackBar('Sucess','done')
              this._dialog.close(true);
            },
            error: (err: any) => {
              console.log(err)
            }
          });
      }
      else{
      console.log(this.personForm.value);
      this._personService.addPerson(this.personForm.value).subscribe(
        {
          next: (val: any) => {
            this._coreService.openSnackBar('Sucess','done')
            this._dialog.close(true);
          },
          error: (err: any) => {
            console.log(err)
          }
        });
      }
    }
  }

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


}
