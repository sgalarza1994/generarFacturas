export interface Response
{
    success:boolean;
    message:string;
}

export interface IResponse<T>
{
  success:boolean;
  message:string;
  result:T;
}
