import {generateOptions} from "~/utils/helpers/formOptionsHelper";
import {TypeLogement} from "~/models/enums/typeLogement";
import {NombreDePieces} from "~/models/enums/nombrePieces";
import {NombreChambres} from "~/models/enums/nombreChambres";
import {NombreSdo} from "~/models/enums/nombreSdo";
import {NombreGarages} from "~/models/enums/nombreGarages";
import {SortOrder} from "~/models/enums/sortOrder";
import {Gender} from "~/models/enums/gender";
import {Chauffage} from "~/models/enums/chauffage";
import type {Option} from "~/models/types/option";


export const types = generateOptions(TypeLogement, {
    value: (item) => item.key.toLowerCase(),
    label: (item) => item.value,
});

export const nombreDePieces = generateOptions(NombreDePieces, {
    value: (item) => item.value,
    label: (item) => item.value,
});

export const nombreDeChambres = generateOptions(NombreChambres, {
    value: (item) => item.value,
    label: (item) => item.value,
});

export const nombreSdo = generateOptions(NombreSdo, {
    value: (item) => item.value,
    label: (item) => item.value,
})

export const nombreGarages = generateOptions(NombreGarages, {
    value: (item) => item.value,
    label: (item) => item.value,
})

export const sortOrderOptions = generateOptions(SortOrder, {
    value: (item) => item.key.toLowerCase(),
    label: (item) => item.value,
});


export const genderOptions = generateOptions(Gender, {
    value: (item) => item.key.toLowerCase(),
    label: (item) => item.value,
});

export const chauffageOptions = generateOptions(Chauffage, {
    value: (item) => item.key.toLowerCase(),
    label: (item) => item.value,
});


interface OptionPerformance extends Option {
    conso: string;
    color: string;
}

export const performanceEnergetique: OptionPerformance[] = [
    {
        value: 'A',
        label: 'A',
        conso: 'moins de 70 kWh',
        color: '#379932',
    },
    {
        value: 'B',
        label: 'B',
        conso: '71 kWh à 110 kWh',
        color: '#3ACC31',
    },
    {
        value: 'C',
        label: 'C',
        conso: '111 kWh à 180 kWh',
        color: '#CDFD33',
    },
    {
        value: 'D',
        label: 'D',
        conso: '181 kWh à 250 kWh',
        color: '#FBEA49',
    },
    {
        value: 'E',
        label: 'E',
        conso: '251 kWh à 330 kWh',
        color: '#FCCC2F',
    },
    {
        value: 'F',
        label: 'F',
        conso: '331 kWh à 420 kWh',
        color: '#FB9C34',
    },
    {
        value: 'G',
        label: 'G',
        conso: '+ de 421 kWh',
        color: '#FA1C1F',
    },
]


export const energyClasses = [
    {
        name: 'A',
        color: '#379932'
    },
    {
        name: 'B',
        color: '#3ACC31'
    },
    {
        name: 'C',
        color: '#CDFD33'
    },
    {
        name: 'D',
        color: '#FBEA49'
    },
    {
        name: 'E',
        color: '#FCCC2F'
    },
    {
        name: 'F',
        color: '#FB9C34'
    },
    {
        name: 'G',
        color: '#FA1C1F'
    }
]

export const gesClasses = [
    {
        name: 'A',
        color: '#F6EDFE'
    },
    {
        name: 'B',
        color: '#E4C7FB'
    },
    {
        name: 'C',
        color: '#D2ADF1'
    },
    {
        name: 'D',
        color: '#C99AEF'
    },
    {
        name: 'E',
        color: '#B77AE9'
    },
    {
        name: 'F',
        color: '#A659E9'
    },
    {
        name: 'G',
        color: '#8835D9'
    }
]

export const typeOptions: Option[] = [
    { label: 'Appartement', value: 'Appartement', icon: 'i-custom-building' },
    { label: 'Maison', value: 'Maison', icon: 'i-custom-house' },
    { label: 'Parking', value: 'Parking', icon: 'i-custom-parking' },
    { label: 'Terrain', value: 'Terrain', icon: 'i-custom-land' },
];

export const caracteristiqueOptions = [
    {
        label: "Parking",
        value: "parking",
        icon: "i-custom-parking"
    },
    {
        label: "Cuisine équipée",
        value: "cuisine-equipee",
        icon: "i-custom-kitchen"
    },
    {
        label: "Transport en commun à proximité",
        value: "transport-commun",
        icon: "i-custom-bus"
    },
    {
        label: "Salle de sport à proximité",
        value: "salle-sport",
        icon: "i-custom-gym"
    },
    {
        label: "Recharge véhicule électrique",
        value: "recharge-electrique",
        icon: "i-custom-charging-station"
    },
    {
        label: "Commerce à proximité",
        value: "commerce",
        icon: "i-custom-cart"
    },
    {
        label: "Double vasque",
        value: "double-vasque",
        icon: "i-custom-sink"
    },
    {
        label: "Laverie à proximité",
        value: "laverie",
        icon: "i-custom-laundry"
    },
    {
        label: "Wifi",
        value: "wifi",
        icon: "i-custom-wifi"
    },
    {
        label: "Résidence sécurisée",
        value: "residence-securisee",
        icon: "i-custom-security"
    },
    {
        label: "Terrasse",
        value: "terrasse",
        icon: "i-custom-terrace"
    },
    {
        label: "École à proximité",
        value: "ecole",
        icon: "i-custom-school"
    },
    {
        label: "Salle de bain",
        value: "salle-de-bain",
        icon: "i-custom-bathroom"
    },
    {
        label: "Balcon",
        value: "balcon",
        icon: "i-custom-balcony"
    },
    {
        label: "Piscine",
        value: "piscine",
        icon: "i-custom-pool"
    },
    {
        label: "Ascenseur",
        value: "ascenseur",
        icon: "i-custom-elevator"
    },
    {
        label: "Accessible aux fauteuils roulants",
        value: "accessible-pmr",
        icon: "i-custom-wheelchair"
    },
    {
        label: "Grenier",
        value: "grenier",
        icon: "i-custom-attic"
    },
    {
        label: "Local poubelle",
        value: "local-poubelle",
        icon: "i-custom-trash"
    },
    {
        label: "Jardin",
        value: "jardin",
        icon: "i-custom-watering-can"
    },
    {
        label: "Local vélo",
        value: "local-velo",
        icon: "i-custom-bike"
    }
];