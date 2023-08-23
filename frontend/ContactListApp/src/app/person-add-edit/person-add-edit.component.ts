import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, AbstractControl } from '@angular/forms';
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
      name: ['', Validators.required],
      sex: '',
      age: ['', [Validators.required, Validators.min(0), Validators.max(150)]],
      contacts: this.fb.array([]),
    });


  }

  emailFormatValidator(control: AbstractControl): { [key: string]: any } | null {
    const emailPattern = /^\S+@\S+\.\S+$/;
    const valid = emailPattern.test(control.value);
    return valid ? null : { invalidEmail: true };
  }

  phoneFormatValidator(control: AbstractControl): { [key: string]: any } | null {
    const phonePattern = /^[789][0-9]{9}$/;
    const valid = phonePattern.test(control.value);
    return valid ? null : { invalidPhone: true };
  }

  ngOnInit(): void {
    this.personForm.patchValue (this.data);
    let contactsFormArray = (this.personForm.get('contacts') as FormArray);
    if (this.data){
      while (contactsFormArray.controls.length > 0) {
        contactsFormArray.removeAt(0);
      }

      // Loop through this.data.contacts and add controls to the 'contacts' FormArray
      for (const contact of this.data.contacts) {
        contactsFormArray.push(
          this.fb.group({
            type: [contact.type],
            contactValue: [contact.contactValue],
          })
        );
      }
      // In your FormArray, apply the custom validator to the contactValue field
      // contactsFormArray.controls.forEach((control) => {

      //   if (control.get('type')?.value=='Email'){
      //     alert(control.get('type')?.value);
      //     control.get('contactValue')?.setValidators([Validators.required,this.emailFormatValidator]);
      //   }else{
      //     control.get('contactValue')?.setValidators([Validators.required,this.phoneFormatValidator]);
      //   }

      // });

    }
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
