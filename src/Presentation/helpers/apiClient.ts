export async function get(path: string) {
    return (await fetch(path)).json();
}
export async function post(path: string, body: any) {
    console.log(JSON.stringify(body))
    return (await fetch(path, { 
        method: 'POST', 
        body: JSON.stringify(body), 
        headers: {
            'Content-Type': 'application/json'
        } })).json();
}