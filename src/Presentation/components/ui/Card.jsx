export function Card({content, description, size = "w-1/3"}) {
    return (
        <div className={`flex flex-wrap items-center rounded bg-white shadow p-6 ${size}`}>
            <p className="block text-white bg-violet-800 py-4 px-6 text-xl rounded-full">{content}</p>
            <p className="block ml-4 text-xl font-light">{description}</p>
        </div>
    )
}