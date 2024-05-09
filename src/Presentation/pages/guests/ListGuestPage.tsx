import { get } from '../../helpers/apiClient'
import React from "react";
import { Form, useLoaderData, useNavigate } from "react-router-dom";
import { Reservation } from "../../components/ui/Reservation";
import { Button } from "../../components/form/Button";
import { InputDate } from "../../components/form/InputDate";

type GuestResponse = {
    id: string,
    name: string,
    email: string,
    phone: string,
}

export async function loader({ request }: {request: Request}) {
    const url = new URL(request.url);
    const page = url.searchParams.get("page") ?? '';
    const limit = url.searchParams.get("limit") ?? '';

    return await get(`/api/guests?page=${page}&limit=${limit}`);
}

export function ListGuestPage() {
    const guestList = useLoaderData() as GuestResponse[];
    const navigate = useNavigate();

    return (
        <div className="w-full">
            <div className="flex items-center justify-between h-16 border-b border-gray-200">
                <p className="px-6 text-xl">Hóspedes</p>
                <div className='flex items-center gap-4 cursor-pointer'>
                    <Form id="search-form" role="search" className="flex gap-2 items-center mr-1">
                        <InputDate name={'from'} />
                        <p className="text-gray-500">-</p>
                        <InputDate name={'to'} />
                        <Button>BUSCAR</Button>
                    </Form>
                </div>
            </div>
            <div className="w-full px-6 mt-10">
                {guestList.map(guest => (
                    <div className="flex justify-between border-b border-gray-100 mb-2 py-1">
                    <p><b>{guest.name}</b> ({guest.email}) | {guest.phone}</p>
                    <div className="flex gap-4">
                        <Button onClick={() => { navigate(`/guests/${guest.id}`) }}>Ver</Button>
                    </div>
                </div>
                ))}
            </div>
        </div>
    );
}
