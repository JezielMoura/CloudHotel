import { Card } from "../../components/ui/Card";
import { get } from '../../helpers/apiClient'
import { Reservation } from "../../components/ui/Reservation";
import { useLoaderData } from "react-router-dom";

export async function loader() {
    return await get(`/api/reservations/summary`);
}

export function Home() {
    const summary = useLoaderData();

    return (
        <div>
            <div className="flex gap-12 justify-center w-full px-16 py-16">
                <Card content={summary.arrival} description={'Chegadas'} />
                <Card content={summary.departure} description={'Saídas'} />
                <Card content={summary.inHouse} description={'Hospedados'} />
            </div>
            <div className="w-full px-16">
                <p className="pb-2 text-xl border-b border-gray-200 mb-4">Reservas de Hoje</p>
                {summary.reservations.map(r => <Reservation reservation={r} key={r.guestName} />)}
            </div>
        </div>
    )
}
