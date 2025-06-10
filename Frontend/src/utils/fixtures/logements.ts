import type {Logement} from "~/models/entities/logement";
import {TypeLogement} from "~/models/enums/typeLogement";
import type {Photo} from "~/models/entities/photo";

const logementTypes = Object.values(TypeLogement);

const getRandomImages = (count: number): Photo[] => {
    const images = [];
    for (let i = 0; i < count; i++) {
        images.push({
            id: Math.floor(Math.random() * 1000),
            uuid: crypto.randomUUID(),
            url: `https://picsum.photos/500/500?random=${Math.floor(Math.random() * 1000)}`,
            description: `Image ${i + 1}`
        });
    }
    return images;
};

const createRandomLogement = (): Logement => {
    const randomType = logementTypes[Math.floor(Math.random() * logementTypes.length)];
    return {
        id: Math.floor(Math.random() * 1000),
        uuid: crypto.randomUUID(),
        adresse: {
            numeroRue: `${Math.floor(Math.random() * 1000)}`,
            nomRue: `Street ${Math.floor(Math.random() * 100)}`,
            complementAdresse: `Apt ${Math.floor(Math.random() * 100)}`,
            quartier: `Quarter ${Math.floor(Math.random() * 10)}`,
            ville: `City ${Math.floor(Math.random() * 100)}`,
            codePostal: `${Math.floor(Math.random() * 90000) + 10000}`,
            region: `Region ${Math.floor(Math.random() * 10)}`,
            pays: `Country ${Math.floor(Math.random() * 10)}`
        },
        superficie: Math.floor(Math.random() * 200) + 20,
        nombreDePieces: Math.floor(Math.random() * 10) + 1,
        meuble: Math.random() < 0.5,
        loyer: Math.round(Math.random() * 2000 + 500),
        charge: Math.round(Math.random() * 200 + 50),
        garantie: Math.round(Math.random() * 2000 + 500),
        sdo: Math.floor(Math.random() * 5),
        chambre: Math.floor(Math.random() * 5),
        garage: Math.floor(Math.random() * 3),
        etage: Math.floor(Math.random() * 10),
        etageMax: Math.floor(Math.random() * 20),
        description: 'A beautiful place to live.',
        type: randomType,
        // classeEnergie: random entre a et g
        classeEnergie: String.fromCharCode(65 + Math.floor(Math.random() * 7)),
        ges: String.fromCharCode(65 + Math.floor(Math.random() * 7)),
        // Random position on Rennes, France
        position: [48.117266 + Math.random() * 0.1, -1.677792 + Math.random() * 0.1],
        proprietaireId: Math.floor(Math.random() * 1000),
        locataireId: Math.floor(Math.random() * 1000),
        options: [],
        // Random number of images between 4 and 8
        photos: getRandomImages(Math.floor(Math.random() * 5) + 20)
    };
};

export const logementFixtures: Logement[] = Array.from({length: 18}, createRandomLogement);

