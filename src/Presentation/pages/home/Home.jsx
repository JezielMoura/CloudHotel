import { Card } from "../../components/ui/Card";
import { use, Suspense } from "react";
import { get } from '../../helpers/apiClient'
import { Reservation } from "../../components/ui/Reservation";

export function Home() {
    const summaryPromisse = async () => get(`/api/reservations/summary`)

    return (
        <div>
            <Suspense fallback={<p>Loadding...</p>}>
                <HomeContent summaryPromisse={summaryPromisse()} />
            </Suspense>
        </div>
    )
}

export function HomeContent({summaryPromisse}) {
    const summary = use(summaryPromisse);

    return (
        <div>
            <div className="flex gap-12 justify-center w-full px-16 py-16">
                <Card content={summary.arrival} description={'Chegadas'} />
                <Card content={summary.departure} description={'Saídas'} />
                <Card content={summary.inHouse} description={'Hospedados'} />
            </div>
            <div className="w-full px-16">
                <p className="pb-2 text-xl">Reservas de Hoje</p>
                {summary.reservations.map(r => <Reservation reservation={r} key={r.guestName} />)}
            </div>
        </div>
    )
}