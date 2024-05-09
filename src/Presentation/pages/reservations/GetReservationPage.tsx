import React from 'react';
import { get, post } from '../../helpers/apiClient';
import { redirect, useLoaderData } from 'react-router';
import { Form } from 'react-router-dom';
import { PageHeader } from "../../components/layout/PageHeader";
import { currencyFormat } from "../../helpers/currencyFormat";
import { isoBrazilFormat } from "../../helpers/dateFormat";

type GetReservationPageResponse = {
  id: string,
  guestName: string,
  arrival: string,
  price: number,
  totalNights: number,
  nightPrice: number,
  departure: string,
  roomCode: string,
  createdOn: string
}
export async function loader({ params }: { params: any }) {
  return await get(`/api/reservations/${params.id}`);
}

export async function action({ request }: {request: Request}) {
  const formData = await request.formData();
  const requestData = Object.fromEntries(formData);
  const sucess = await post('/api/reservations', requestData);

  return sucess ? redirect('/reservations') : null
}

export function GetReservationPage()
{
  const data = useLoaderData() as GetReservationPageResponse;

  return (
    <div>
      <PageHeader title='Detalhes da Reserva'></PageHeader>
      <div className='p-6'>
        <p className='py-1'><b>Nome:</b> {data.guestName}</p>
        <p className='py-1'><b>Check-in:</b> {isoBrazilFormat(data.arrival)}</p>
        <p className='py-1'><b>Check-out:</b> {isoBrazilFormat(data.departure)}</p>
        <p className='py-1'><b>Acomodação:</b> {data.roomCode}</p>
        <p className='py-1'><b>Reservado em:</b> {isoBrazilFormat(data.createdOn)}</p>
        <p className='py-1'><b>Valor total:</b> {currencyFormat(data.price)}</p>
        <p className='py-1'><b>Diária média:</b> {currencyFormat(data.nightPrice)}</p>
        <p className='py-1'><b>Diárias:</b> {data.totalNights}</p>
      </div>
    </div>
  )
}