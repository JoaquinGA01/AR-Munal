Aplicacion de Realidad Aumentada Orientada a Museos
Descargar el SDK de Vuforia (https://developer.vuforia.com/downloads/sdk?_=1714934205) y una vez descargado el repositorio importar el sdk
Para usarlo, solo acceder el objeto Controlador/ControladorInformacion que esta en la jererquia de Unity de la Escena AR-Munal, Agregar con el siguiente formato:

{
            "Nombre_Obra": " ",
            "Panel_Informacion_Izquierdo": "",
            "Panel_Informacion_Derecho": "",
            "Autor": "",
            "Ubicacion": ""
}

una vez agregado presionar el boton, crear carpetas y Dentro de la carpeta Resources y StreamingAssets/Animaciones se va a crear una carpeta con el nombre que hayas agregado en el campo "Nombre_Obra", en Resources Agregar la imagen que se quiere mostrar y en la otra las animaciones que quiere proyectar

Posterior a eso, en la carpeta Prefab´s Agreagr a la jerarquia el componente ImageTarget, y dentro del inspector de este objeto, seleccion de donde se va a obtener el marcador, puede ser desde una bd o desde la misma carpeta de Resource y en el Script unico, agreagr el nombre al cual se esta haciendo referencia, tomando en cuenta que debe se igual al atributo "Nombre_Obra" y a la lista agregar tantas animaciones como se queira con el nombre de cada aniamcion
