import { Button, InputDate, InputSelect, InputText, InputNumber } from "../../components/form/Form";
import React from "react";
import { get, post } from "../../helpers/apiClient";
import { redirect, useLoaderData } from "react-router";
import { Form } from "react-router-dom";
import { PageHeader } from "../../components/layout/PageHeader";

type RoomSummaryResponse = {
    id: string,
    name: string,
    code: string
}

export async function loader({ request }: { request: Request }) {
    return await get('/api/rooms');
}
  
export async function action({ request }: {request: Request}) {
    const formData = await request.formData();
    const requestData = Object.fromEntries(formData) as any;
    const roomDetails = requestData.roomDetails.split(';');
    const sucess = await post('/api/reservations', {...requestData, roomId: roomDetails[0], roomCode: roomDetails[1]});

    return sucess ? redirect('/reservations') : null
}

export function CreateReservationPage() {
    const rooms = useLoaderData() as any;
    const roomsDetails: RoomSummaryResponse[] = [];

    rooms.forEach(roomGroup => {
        roomGroup.rooms.forEach(room => roomsDetails.push({ id: room.id, name: room.name, code: room.code }))
    })

    return (
        <div className="w-full">
            <PageHeader title="Nova Reserva" />
            <Form method="post" className="p-5">
                <div className="flex gap-2">
                    <InputDate name={'arrival'} label={'Chegada'} width="w-full mb-3" required={true} />
                    <InputDate name={'departure'} label={'Saída'} width="w-full mb-3" required={true} />
                    <InputNumber name={'price'} label="Preço" required={true} />
                </div>
                <div className="flex gap-2">
                    <InputText name={'guestName'} label="Nome" />
                    <InputText name={'guestEmail'} label="E-mail" />
                    <InputText name={'guestPhone'} label="Telefone" />
                </div>
                <div className="flex gap-2">
                    <InputText name={'guestDocumentNumber'} label="Documento" />
                    <InputText name={'guestDocumentType'} label="Tipo de Documento" />
                    <InputSelect name={'roomDetails'} label={'Acomodação'}>
                        {roomsDetails.map(x => <option key={x.id} value={`${x.id};${x.code}`}>{x.name} | {x.code}</option>)}
                    </InputSelect>
                </div>
                <div className="flex gap-2">
                    <InputText name={'addressPostalCode'} label="CEP" />
                    <InputText name={'addressStreet'} label="Rua" />
                    <InputText name={'addressCity'} label="Cidade" />
                </div>
                <div className="flex gap-2">
                    <InputText name={'addressState'} label="Estado" />
                    <InputText name={'addressCountry'} label="País" />
                </div>
                <div className="flex justify-center my-3">
                    <Button>SALVAR</Button>
                </div>
            </Form>
        </div>
    )
}
