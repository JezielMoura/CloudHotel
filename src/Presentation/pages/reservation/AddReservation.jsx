import { Button } from "../../components/form/Button";
import { InputDate } from "../../components/form/InputDate";
import { InputSelect } from "../../components/form/InputDateSelect";
import { InputText } from "../../components/form/InputText";
import React from "react";
import { use } from "react";
import { post } from "../../helpers/apiClient";

export function AddReservation({roomsPromisse}) {
    const rooms = use(roomsPromisse);
    const roomsDetails = [];

    rooms.forEach(roomGroup => {
        roomGroup.rooms.forEach(room => roomsDetails.push({ id: room.id, name: room.code }))
    })

    const submitAction = async (formData) => {
        const requestData = Object.fromEntries(formData);
        await post('/api/reservations', requestData);
    }

    return (
        <div className="fixed w-full h-full top-0 left-0 flex justify-center items-center bg-gray-900/50">
            <div className="w-80 bg-white shadow rounded">
                <div className="flex justify-between p-2 border-b border-gray-200">
                    <p>Nova Reserva</p>
                    <img src="/icons/close.svg" alt="close" />
                </div>
                <form action={submitAction} className="px-5">
                    <div className="flex justify-between mt-3">
                        <InputDate name={'arrival'} />
                        <InputDate name={'departure'} />
                    </div>
                    <InputText name={'price'} label="Preço" />
                    <InputText name={'guestName'} label="Nome" />
                    <InputText name={'guestEmail'} label="E-mail" />
                    <InputText name={'guestPhone'} label="Telefone" />
                    <InputText name={'guestDocumentNumber'} label="Documento" />
                    <InputText name={'guestDocumentType'} label="Tipo de Documento" />
                    <InputSelect name={'roomDetails'}>
                        {roomsDetails.map(x => <option key={x.id} value={`${x.id};${x.name}`}>{x.name}</option>)}
                    </InputSelect>
                    <div className="flex justify-center my-3">
                        <Button>SALVAR</Button>
                    </div>
                </form>
            </div>
        </div>
    )
}
