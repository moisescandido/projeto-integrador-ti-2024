import requests
import random
import time

def get_random_temperature():
    return round(random.uniform(200, 350) / 10)  # Multiplica por 10 para obter um número inteiro entre 20 e 35

def get_random_humidity():
    return round(random.uniform(20, 350) / 10)  # Multiplica por 10 para obter um número inteiro entre 50 e 80

def test_api():
    while True:
        temperatura = get_random_temperature()
        umidade = get_random_humidity()
        endpoint = f"http://localhost:5091/clima/{temperatura}/{umidade}"

        token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlJhc3BiZXJyeSBQaWNvIFciLCJuYmYiOjE3MTAwOTAyNTgsImV4cCI6MTc3MzE2MjI1OCwiaWF0IjoxNzEwMDkwMjU4fQ.yFKew7HedDnwNjfKCp3_JGWxReBN_v3vCyfEjfcjyLY"
        headers = {"Content-Type": "application/json",
                   "Authorization": token}
        
        response = requests.get(endpoint, headers=headers)
        try:
            response = requests.post(endpoint)
            response.raise_for_status()  # Lança uma exceção se a resposta não for bem-sucedida (status diferente de 2xx)
            print("Requisição enviada para", endpoint)
            print("Resposta:", response.text)
        except requests.exceptions.RequestException as e:
            print("Erro na requisição:", e)
        
        # Aguarda um tempo antes de enviar a próxima requisição
        time.sleep(5)

if __name__ == "__main__":
    test_api()
