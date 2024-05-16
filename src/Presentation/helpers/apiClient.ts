import { toast } from 'react-toastify';

export async function get(path: string) {
  return (await fetch(path)).json();
}

export async function post(path: string, data: any) {
  const response = await fetch(path, {
    method: "POST",
    body: JSON.stringify(data),
    headers: { 'Content-Type': 'application/json'}
  });

  return await responseHandler(response);
}

export async function put(path: string, data: any) {
  console.log(data);
  const response = await fetch(path, {
    method: "PUT",
    body: JSON.stringify(data),
    headers: { 'Content-Type': 'application/json'}
  });

  return await responseHandler(response);
}

async function responseHandler(response: Response) {
    if (response.status == 204)
      return true;
  
    if (response.status == 200)
      return await response.json();
  
    if (response.status == 400)
      (await response.json()).errors.   map((e:any) => toast.info(e.message))
  
    if ([401, 403, 404, 500].includes(response.status))
      throw { code: response.statusText.replace(' ', ''), message: await response.text() }
      
    return null
  }