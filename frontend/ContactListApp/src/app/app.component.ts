import { Component, OnInit, ViewChild} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PersonAddEditComponent } from './person-add-edit/person-add-edit.component';
import { PersonService } from './services/person.service';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatSort, MatSortModule} from '@angular/material/sort';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { CoreService } from './core/core.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ContactListApp';
  displayedColumns: string[] = ['name', 'sex', 'age', 'action'] ;
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor (private _dialog: MatDialog,
    private _personService: PersonService,
    private _coreService: CoreService
    ) {

    }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  ngOnInit(): void {
    this.getPersonList();

  }

  openAddEditPersonForm(){
    const dialogRef = this._dialog.open(PersonAddEditComponent);
    dialogRef.afterClosed().subscribe(
      {
        next: (val: any) => {
          if (val) {
            this.getPersonList();
          }
        },
        error: (err: any) => {
          console.log(err)
        }
      });


  }

  getPersonList(){
    this._personService.getPersonList().subscribe(
      {
        next: (val: any) => {
          console.log(val);
          this.dataSource = new MatTableDataSource(val);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        },
        error: (err: any) => {
          console.log(err)
        }
      });
  }

  deletePerson(id: string){
    this._personService.deletePerson(id).subscribe(
      {
        next: (val: any) => {
          this._coreService.openSnackBar('Deleted','done')
          this.getPersonList();
        },
        error: (err: any) => {
          console.log(err)
        }
      });
  }

  openEditPersonForm(data: any){
    const dialogRef = this._dialog.open(PersonAddEditComponent, { data, } );
    dialogRef.afterClosed().subscribe(
      {
        next: (val: any) => {
          if (val) {
            this.getPersonList();
          }
        },
        error: (err: any) => {
          console.log(err)
        }
      });


  }

}
