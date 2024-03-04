import urequests
from utime import sleep
from machine import Pin
import network
import dht


def connect():
    ssid = 'admin'
    password = 'admin'
    wlan = network.WLAN(network.STA_IF)
    wlan.active(True)
    wlan.connect(ssid, password)

    while wlan.isconnected() == False:
        print('Waiting for connection...')
        sleep(100)

    ip = wlan.ifconfig()[0]
    print(f'Connected on {ip}')

def enviar_dados(temperatura, umidade):
        url = 'https://raspti-web.azurewebsites.net/clima?temperatura=34&umidade=67'
        try:
            response = urequests.post(url)
            if response.status_code == 200:
                print("Dados enviados com sucesso")
                print("Umidade: {} : Temperatura: {}".format(umidade, temperatura))
            else:
                print("Erro ao enviar dados:", response.status_code)
        except Exception as e:
            print("Erro ao enviar requisição:", e)

def temperatura_umidade():
    while True:
        sensor = dht.DHT22(Pin(2))
        sensor.measure()  
        enviar_dados(sensor.temperature(),sensor.humidity())
        sleep(5)

connect()
sleep(5)
temperatura_umidade()
