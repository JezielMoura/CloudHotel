import { get } from '../../helpers/apiClient'
import React from "react";
import { Form, useLoaderData, useNavigate } from "react-router-dom";
import { Reservation } from "../../components/ui/Reservation";
import { Button } from "../../components/form/Button";
import { InputDate } from "../../components/form/InputDate";

export async function loader({ request }: {request: Request}) {
    const url = new URL(request.url);
    const from = url.searchParams.get("from") ?? '';
    const to = url.searchParams.get("to") ?? '';

    return await get(`/api/reservations?arrivalFrom=${from}&arrivalTo=${to}`);
}

export function ListReservationPage() {
    const reservations = useLoaderData() as any;
    const navigate = useNavigate();

    return (
        <div className="w-full">
            <div className="flex items-center justify-between h-16 border-b border-gray-200">
                <p className="px-6 text-xl">Reservas</p>
                <div className='flex items-center gap-4 cursor-pointer'>
                    <div className='text-violet-900' onClick={() => navigate('/reservations/add')}>
                        <img className='h-8' src="/icons/add.svg" alt="" />
                    </div>
                    <Form id="search-form" role="search" className="flex gap-2 items-center mr-1">
                        <InputDate name={'from'} />
                        <p className="text-gray-500">-</p>
                        <InputDate name={'to'} />
                        <Button>BUSCAR</Button>
                    </Form>
                </div>
            </div>
            <div className="w-full px-6 mt-10">
                {reservations.map(r => <Reservation reservation={r} key={r.id} />)}
            </div>
        </div>
    );
}
