import React from 'react';
import { get } from '../../helpers/apiClient';
import { useLoaderData } from 'react-router';
import { PageHeader } from "../../components/layout/PageHeader";

type GetGuestResponse = {
  id: string,
  name: string,
  email: string,
  phone: string,
  documentNumber: string,
  documentType: string,
  postalCode: string,
  street: string,
  city: string,
  state: string,
  country: string,
}
export async function loader({ params }: { params: any }) {
  return await get(`/api/guests/${params.id}`);
}

export function GetGuestPage() {
  const data = useLoaderData() as GetGuestResponse;

  return (
    <div>
      <PageHeader title='Detalhes do Hóspede'></PageHeader>
      <div className='p-6'>
        <p className='py-1'><b>Nome:</b> {data.name}</p>
        <p className='py-1'><b>E-mail:</b> {data.email}</p>
        <p className='py-1'><b>Telefone:</b> {data.phone}</p>
        <p className='py-1'><b>Documento:</b> {data.documentNumber} ({data.documentType})</p>
        <p className='py-1'><b>CEP:</b> {data.postalCode}</p>
        <p className='py-1'><b>Rua:</b> {data.street}</p>
        <p className='py-1'><b>Cidade:</b> {data.city}</p>
        <p className='py-1'><b>Estado:</b> {data.state}</p>
        <p className='py-1'><b>País:</b> {data.country}</p>
      </div>
    </div>
  )
}