import React from 'react';
import { Button, InputDate, InputSelect, InputText, InputNumber } from "../../components/form/Form";
import { get, put } from '../../helpers/apiClient';
import { redirect, useLoaderData } from 'react-router';
import { Form } from 'react-router-dom';
import { PageHeader } from "../../components/layout/PageHeader";

type UpdateReservationResponse = {
  id: string,
  guestId: string,
  guestName: string,
  guestEmail: string,
  guestPhone: string,
  guestDocumentNumber: string,
  guestDocumentType: string,
  addressPostalCode: string,
  addressStreet: string,
  addressCity: string,
  addressState: string,
  addressCountry: string,
  arrival: string,
  price: number,
  totalNights: number,
  nightPrice: number,
  departure: string,
  roomId: string,
  roomCode: string,
  createdOn: string,
  status: number,
  reservationStatuses: ReservationStatus[]
}

type RoomGroupSummaryResponse = {
  id: string,
  rooms: RoomSummaryResponse[]
}

type RoomSummaryResponse = {
  id: string,
  name: string,
  code: string
}

type LoaderDataResponse = {
  roomGroups: RoomGroupSummaryResponse[],
  reservation: UpdateReservationResponse
}

type ReservationStatus = {
  value: number,
  description: string
}

export async function loader({ params }: { params: any }) {
  const roomGroups = await get('/api/rooms');
  const reservation = await get(`/api/reservations/${params.id}`);

  return { roomGroups, reservation }
}

export async function action({ request }: {request: Request}) {
  const formData = await request.formData();
  const requestData = Object.fromEntries(formData);
  const sucess = await put('/api/reservations', requestData);

  return sucess ? redirect('/reservations') : null
}

export function UpdateReservationPage()
{
  const data = useLoaderData() as LoaderDataResponse;
  const reservation = data.reservation;
  const roomGroups = data.roomGroups;

  const roomsDetails: RoomSummaryResponse[] = [];

  roomGroups.forEach(roomGroup => {
    roomGroup.rooms.forEach(room => roomsDetails.push({ id: room.id, name: room.name, code: room.code }))
  })

  return (
    <div>
      <PageHeader title='Alterar Reserva'></PageHeader>
      <Form method="post" className="p-5">
        <input type="hidden" name="id" value={reservation.id} />
        <input type="hidden" name="guestId" value={reservation.guestId} />
        <input type="hidden" name="roomCode" value={reservation.roomCode} />
        <div className="flex gap-2">
            <InputDate name={'arrival'} label={'Chegada'} width="w-full mb-3" required={true} value={reservation.arrival} />
            <InputDate name={'departure'} label={'Saída'} width="w-full mb-3" required={true} value={reservation.departure} />
            <InputNumber name={'price'} label="Preço" required={true} value={reservation.price} />
            <InputSelect name={'status'} label={'Status'} value={reservation.status.toString()}>
                {reservation.reservationStatuses.map(x => <option key={x.value} value={x.value}>{x.description}</option>)}
            </InputSelect>
        </div>
        <div className="flex gap-2">
            <InputText name={'guestName'} label="Nome" value={reservation.guestName} />
            <InputText name={'guestEmail'} label="E-mail" value={reservation.guestEmail} />
            <InputText name={'guestPhone'} label="Telefone" value={reservation.guestPhone} />
        </div>
        <div className="flex gap-2">
            <InputText name={'guestDocumentNumber'} label="Documento" value={reservation.guestDocumentNumber}  />
            <InputText name={'guestDocumentType'} label="Tipo de Documento" value={reservation.guestDocumentType}  />
            <InputSelect name={'roomId'} label={'Acomodação'} value={reservation.roomId}>
              {roomsDetails.map(x => <option key={x.id} value={x.id}>{x.name} | {x.code}</option>)}
            </InputSelect>
        </div>
        <div className="flex gap-2">
            <InputText name={'addressPostalCode'} label="CEP" value={reservation.addressPostalCode}  />
            <InputText name={'addressStreet'} label="Endereço" value={reservation.addressStreet}  />
            <InputText name={'addressCity'} label="Cidade" value={reservation.addressCity}  />
        </div>
        <div className="flex gap-2">
            <InputText name={'addressState'} label="Estado" value={reservation.addressState}  />
            <InputText name={'addressCountry'} label="País" value={reservation.addressCountry}  />
        </div>
        <div className="flex justify-center my-3">
            <Button>SALVAR</Button>
        </div>
    </Form>
    </div>
  )
}