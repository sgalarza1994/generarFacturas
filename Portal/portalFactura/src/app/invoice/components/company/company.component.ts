import { Component, OnInit,TemplateRef,AfterViewInit ,ViewChild} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/core/services/loader.service';
import { TokenService } from 'src/app/core/services/token.service';
import { ICompany } from 'src/app/models/home/company.interface';
import { CompanyService } from 'src/app/services/company.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements OnInit,AfterViewInit {

  constructor(
    private loaderService:LoaderService,
    private tokenService:TokenService,
    private modalService: BsModalService,
    private fb:FormBuilder,
    private companyService:CompanyService,
    private toashService:ToastrService,) { }
    companySeleccionado:ICompany | undefined;
    dataSource = new MatTableDataSource<ICompany>;
    displayedColumns: string[] = ['businessName', 'address', 'phone', 'email', 'action'];
    formularioCompany:FormGroup = new FormGroup({});
    config = {
      backdrop: true,
      ignoreBackdropClick: true,
      keyboard: false
    };
     //@ts-ignore
     @ViewChild(MatPaginator,{static:true}) paginator: MatPaginator;
     //@ts-ignore
     @ViewChild(MatSort,{static:true}) sort: MatSort;
    //@ts-ignore
    public modalRef: BsModalRef;
  ngOnInit(): void {
    this.estructuraFormulario();
  }

  ngAfterViewInit() {
    this.getCompany();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  public openModalOpc(template: TemplateRef<any>, bandera: ICompany,detalle:boolean) {
    if(detalle)
    {
      this.companySeleccionado = bandera;

    }
    this.formularioCompany.controls['businessName'].setValue(bandera.businessName);
    this.formularioCompany.controls['email'].setValue(bandera.email);
    this.formularioCompany.controls['phone'].setValue(bandera.phone);
    this.formularioCompany.controls['address'].setValue(bandera.address);
    this.formularioCompany.controls['ruc'].setValue(bandera.ruc);
    this.modalRef = this.modalService.show(template, this.config);

  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getCompany()
  {
    let users = this.tokenService.getUser();
    let admin = users.rolId == 1;
  //  this.loaderService.isLoading.next(true);
    this.companyService.getCompany(users.userId,admin).subscribe({
      next : (response) =>
      {
         if(response)
         {
          if(response.success)
          {
            this.dataSource.data = response.result;
          }
         }
      },
      error : (error:any) =>
      {

      },
      complete : ()  =>
      {
      //  this.loaderService.isLoading.next(false);
      }
    })

  }
  editarCompany()
  {
    this.loaderService.isLoading.next(true);

    let request : ICompany =
    {
      address : this.formularioCompany.value.address,
      businessName : this.formularioCompany.value.businessName,
      companyId : this.companySeleccionado?.companyId!,
      email : this.formularioCompany.value.email,
      phone : this.formularioCompany.value.phone,
      ruc : this.formularioCompany.value.ruc

    };
    this.companyService.addCompany(request).subscribe({
      next : (response) =>
      {
        if(response)
        {
          if(response.success)
          {
            this.getCompany();
            this.formularioCompany.reset();
            this.modalRef.hide();
          }
          else
          {
            this.toashService.error(response.message);
          }
        }
      },
      error : (error:any) =>
      {

      },
      complete : () =>
      {
        this.loaderService.isLoading.next(false);
      }
    });
  }
  estructuraFormulario()
  {
    this.formularioCompany = this.fb.group({
      businessName: ['',Validators.compose([Validators.required])],
      address : [''],
      phone : [''],
      email : [''],
      ruc : ['']
    })


  }

  get email()
  {
    return this.formularioCompany.get('email');
  }

  get ruc()
  {
    return this.formularioCompany.get('ruc');
  }

  get businessName()
  {
    return this.formularioCompany.get('businessName');
  }

  get phone()
  {
    return this.formularioCompany.get('phone');
  }
  get address()
  {
    return this.formularioCompany.get('address');
  }
}
